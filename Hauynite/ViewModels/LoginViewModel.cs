using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
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

		public IObservable<Result> LoginAsync()
		{
			return Observable.Create((IObserver<Result> observer) => {
				phoneFeatureService.LoginFinished += (result) =>
				{
					observer.OnNext(result);
					observer.OnCompleted();
				};
				phoneFeatureService.Login();
				return Disposable.Empty;
			});
		}
	}
}
