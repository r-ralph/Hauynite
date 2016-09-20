using System;
using Android.Content;

namespace Hauynite.Droid
{
	[BroadcastReceiver]
	public class FacebookLoginReceiver : BroadcastReceiver
	{
		public const string ActionKey = "FacebookLoginReceiver.Action";
		public const string ExtraResult = "FacebookLoginReceiver.Result";

		public event Action<Result> LoginFinished;

		public override void OnReceive(Context context, Intent intent)
		{
			var result = Result.Unknown;

			if (intent.Action != ActionKey)	return;

			if (intent.Extras != null && intent.Extras.ContainsKey(ExtraResult))
			{
				result = (Result)Enum.ToObject(typeof(Result), intent.Extras.GetInt(ExtraResult));
				intent.RemoveExtra(ExtraResult);
			}

			if (LoginFinished != null)
			{
				LoginFinished(result);
			}
		}
	}
}
