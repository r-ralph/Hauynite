using System;
using Android.Content;
using Hauynite.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidPhoneFeatureService))]

namespace Hauynite.Droid
{
	public class AndroidPhoneFeatureService : IPhoneFeatureService
	{
		public event Action<Result, string> LoginFinished;

		public void Login()
		{
			var context = Forms.Context;
			if (!(context is MainActivity)) return;
			var activity = context as MainActivity;

			// Register BroadcastReceiver
			var receiver = new FacebookLoginReceiver();
			receiver.LoginFinished += (result, userId) =>
			{
				context.UnregisterReceiver(receiver);
				if (LoginFinished != null)
				{
					LoginFinished(result, userId);
				}
			};
			context.RegisterReceiver(receiver, new IntentFilter(FacebookLoginReceiver.ActionKey));

			// Start
			activity.client.Login();
		}

		public event Action<Result, string, string> GetOwnNameFinished;

		public void GetOwnName()
		{
			var context = Forms.Context;
			if (!(context is MainActivity)) return;
			var activity = context as MainActivity;

			// Register BroadcastReceiver
			var receiver = new FacebookOwnNameReceiver();
			receiver.GetOwnNameFinished += (result, name, error) =>
			{
				context.UnregisterReceiver(receiver);
				if (GetOwnNameFinished != null)
				{
					GetOwnNameFinished(result, name, error);
				}
			};
			context.RegisterReceiver(receiver, new IntentFilter(FacebookOwnNameReceiver.ActionKey));

			// Start
			activity.client.GetOwnName();
		}
	}
}
