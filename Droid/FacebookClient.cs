using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Org.Json;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;

namespace Hauynite.Droid
{
	public class FacebookClient
	{
		MainActivity activity;

		ICallbackManager callbackManager;

		AccessToken token;

		public FacebookClient(MainActivity activity)
		{
			this.activity = activity;
		}

		/**
		 * Init Facebook
		 */
		public void Init()
		{
			FacebookSdk.SdkInitialize(activity.ApplicationContext);
			FacebookSdk.ApplicationId = activity.Resources.GetString(Resource.String.facebook_app_id);
			callbackManager = CallbackManagerFactory.Create();
			var loginCallback = new FacebookCallback<LoginResult>
			{
				HandleSuccess = loginResult =>
				{
					token = loginResult.AccessToken;
					activity.CreateDatabase(token.UserId);
					var intent = new Intent(FacebookLoginReceiver.ActionKey);
					intent.PutExtra(FacebookLoginReceiver.ExtraResult, (int)Result.Success);
					intent.PutExtra(FacebookLoginReceiver.ExtraUserId, token.UserId);
					activity.SendBroadcast(intent);
				},
				HandleCancel = () =>
				{
					var intent = new Intent(FacebookLoginReceiver.ActionKey);
					intent.PutExtra(FacebookLoginReceiver.ExtraResult, (int)Result.Cancel);
					activity.SendBroadcast(intent);
				},
				HandleError = loginError =>
				{
					var intent = new Intent(FacebookLoginReceiver.ActionKey);
					intent.PutExtra(FacebookLoginReceiver.ExtraResult, (int)Result.Error);
					activity.SendBroadcast(intent);
				}
			};

			LoginManager.Instance.RegisterCallback(callbackManager, loginCallback);
		}

		public void OnActivityResult(int requestCode, int resultCode, Intent data)
		{
			callbackManager.OnActivityResult(requestCode, resultCode, data);
		}

		// API

		internal void Login()
		{
			LoginManager.Instance.LogInWithReadPermissions(activity, new List<string> { "email", "user_friends" });
		}

		internal void GetOwnName()
		{
			var callback = new GraphJSONObjectCallback
			{
				HandleSuccess = obj =>
				{
					var intent = new Intent(FacebookOwnNameReceiver.ActionKey);
					intent.PutExtra(FacebookOwnNameReceiver.ExtraResult, (int)Result.Success);
					intent.PutExtra(FacebookOwnNameReceiver.ExtraName, obj.GetString("name"));
					activity.SendBroadcast(intent);
				},
				HandleError = error =>
				{
					var intent = new Intent(FacebookOwnNameReceiver.ActionKey);
					intent.PutExtra(FacebookOwnNameReceiver.ExtraResult, (int)Result.Error);
					intent.PutExtra(FacebookOwnNameReceiver.ExtraError, error.ToString());
					activity.SendBroadcast(intent);
				}
			};
			GraphRequest request = GraphRequest.NewMeRequest(token, callback);
			request.ExecuteAsync();
		}
	}

	class FacebookCallback<TResult> : Java.Lang.Object, IFacebookCallback where TResult : Java.Lang.Object
	{
		public Action HandleCancel { get; set; }
		public Action<FacebookException> HandleError { get; set; }
		public Action<TResult> HandleSuccess { get; set; }

		public void OnCancel()
		{
			var c = HandleCancel;
			if (c != null)
				c();
		}

		public void OnError(FacebookException error)
		{
			var c = HandleError;
			if (c != null)
				c(error);
		}

		public void OnSuccess(Java.Lang.Object result)
		{
			var c = HandleSuccess;
			if (c != null)
				c(result.JavaCast<TResult>());
		}
	}

	class GraphJSONObjectCallback : Java.Lang.Object, GraphRequest.IGraphJSONObjectCallback
	{

		public Action<JSONObject> HandleSuccess { get; set; }
		public Action<FacebookRequestError> HandleError { get; set; }

		public void OnCompleted(JSONObject jsonObj, GraphResponse response)
		{
			if (response.Error != null)
			{
				var c = HandleError;
				if (c != null)
					c(response.Error);
			}
			else {
				var c = HandleSuccess;
				if (c != null)
					c(jsonObj);
			}
		}
	}
}
