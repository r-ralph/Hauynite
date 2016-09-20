using System;
using Android.Content;
using Hauynite.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidPhoneFeatureService))]

namespace Hauynite.Droid
{
	public class AndroidPhoneFeatureService : IPhoneFeatureService
	{
		public event Action<Result> LoginFinished;

		public void Login()
		{
			var context = Forms.Context;
			if (!(context is MainActivity)) return;
			var activity = context as MainActivity;

			// Register BroadcastReceiver
			var receiver = new FacebookLoginReceiver();
			receiver.LoginFinished += (result) =>
			{
				context.UnregisterReceiver(receiver);
				if (LoginFinished != null)
				{
					LoginFinished(result);
				}
			};
			context.RegisterReceiver(receiver, new IntentFilter(FacebookLoginReceiver.ActionKey));

			// Start
			activity.OnFacebookLoginStart();
		}
	}
}
