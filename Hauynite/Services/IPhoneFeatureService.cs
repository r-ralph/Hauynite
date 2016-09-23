using System;
namespace Hauynite
{
	public enum Result { Success, Cancel, Error, Unknown };

	public interface IPhoneFeatureService
	{
		void Login();
		event Action<Result, string> LoginFinished;

		void GetOwnName();
		event Action<Result, string, string> GetOwnNameFinished;
	}
}
