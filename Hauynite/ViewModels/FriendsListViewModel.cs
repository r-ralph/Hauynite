using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Hauynite.ViewModels;
using Xamarin.Forms;

namespace Hauynite
{
	public class FriendsListViewModel : ViewModelBase
	{
		IPhoneFeatureService phoneFeatureService;

		public FriendsListViewModel()
		{
			phoneFeatureService = DependencyService.Get<IPhoneFeatureService>();
		}

		public IObservable<string> GetOwnNameAsync()
		{
			IsBusy = true;
			return Observable.Create((IObserver<string> observer) =>
			{
				phoneFeatureService.GetOwnNameFinished += (result, name, error) =>
				{
					if (error != null)
					{
						observer.OnError(new Exception(error));
					}
					else {
						observer.OnNext(name);
					}
					IsBusy = false;
					observer.OnCompleted();
				};
				phoneFeatureService.GetOwnName();
				return Disposable.Empty;
			});
		}
	}
}
