using System;
using Android.Content;

namespace Hauynite.Droid
{
	[BroadcastReceiver]
	public class FacebookOwnNameReceiver : BroadcastReceiver
	{
		public const string ActionKey = "FacebookOwnNameReceiver.Action";
		public const string ExtraResult = "FacebookOwnNameReceiver.Result";
		public const string ExtraName = "FacebookOwnNameReceiver.Name";
		public const string ExtraError = "FacebookOwnNameReceiver.Error";


		public event Action<Result, string, string> GetOwnNameFinished;

		public override void OnReceive(Context context, Intent intent)
		{
			var result = Result.Unknown;
			var name = string.Empty;
			var error = string.Empty;

			if (intent.Action != ActionKey) return;

			if (intent.Extras != null && intent.Extras.ContainsKey(ExtraResult))
			{
				result = (Result)Enum.ToObject(typeof(Result), intent.Extras.GetInt(ExtraResult));
				intent.RemoveExtra(ExtraResult);
			}
			if (intent.Extras != null && intent.Extras.ContainsKey(ExtraName))
			{
				name = intent.Extras.GetString(ExtraName);
				error = null;
				intent.RemoveExtra(ExtraName);
			}
			else {
				name = null;
				error = intent.Extras.GetString(ExtraError);
				intent.RemoveExtra(ExtraError);
			}
			if (GetOwnNameFinished != null)
			{
				GetOwnNameFinished(result, name, error);
			}
		}
	}
}
