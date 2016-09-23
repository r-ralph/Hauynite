using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace Hauynite
{
	public class LoginViewModel
	{

		IPhoneFeatureService phoneFeatureService;
		
		public LoginViewModel()
		{
			phoneFeatureService = DependencyService.Get<IPhoneFeatureService>();
		}

		public IObservable<Tuple<Result, string>> LoginAsync()
		{
			return Observable.Create((IObserver<Tuple<Result, string>> observer) => {
				phoneFeatureService.LoginFinished += (result, userId) =>
				{
					observer.OnNext(new Tuple<Result, string>(result, userId));
					observer.OnCompleted();
				};
				phoneFeatureService.Login();
				return Disposable.Empty;
			});
		}
	}
}
