using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using System.Collections.Generic;

namespace Hauynite.Droid
{
	[Activity(Label = "Hauynite.Droid",
	          Icon = "@drawable/icon",
	          Theme = "@style/MyTheme",
	          MainLauncher = true,
	          ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		ICallbackManager callbackManager;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(savedInstanceState);

			InitFacebook();

			Xamarin.Forms.Forms.Init(this, savedInstanceState);

			LoadApplication(new App());
		}

		protected override void OnActivityResult(int requestCode, Android.App.Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
		}

		/**
		 * Init Facebook
		 */
		void InitFacebook()
		{
			FacebookSdk.SdkInitialize(ApplicationContext);
			FacebookSdk.ApplicationId = Resources.GetString(Resource.String.facebook_app_id);
			callbackManager = CallbackManagerFactory.Create();
			var loginCallback = new FacebookCallback<LoginResult>
			{
				HandleSuccess = loginResult =>
				{
					var intent = new Intent(FacebookLoginReceiver.ActionKey);
					intent.PutExtra(FacebookLoginReceiver.ExtraResult, (int)Result.Success);
					SendBroadcast(intent);
				},
				HandleCancel = () =>
				{
					var intent = new Intent(FacebookLoginReceiver.ActionKey);
					intent.PutExtra(FacebookLoginReceiver.ExtraResult, (int)Result.Cancel);
					SendBroadcast(intent);
				},
				HandleError = loginError =>
				{
					var intent = new Intent(FacebookLoginReceiver.ActionKey);
					intent.PutExtra(FacebookLoginReceiver.ExtraResult, (int)Result.Error);
					SendBroadcast(intent);
				}
			};

			LoginManager.Instance.RegisterCallback(callbackManager, loginCallback);
		}

		/**
		 * Start Facebook login
		 */
		internal void OnFacebookLoginStart()
		{
			LoginManager.Instance.LogInWithReadPermissions(this, new List<string> { "email" });
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
}
