using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Xamarin.Forms;

namespace Hauynite
{
	public class FriendsListViewModel
	{
		IPhoneFeatureService phoneFeatureService;

		public FriendsListViewModel()
		{
			phoneFeatureService = DependencyService.Get<IPhoneFeatureService>();
		}

		public IObservable<string> GetOwnNameAsync()
		{
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
					observer.OnCompleted();
				};
				phoneFeatureService.GetOwnName();
				return Disposable.Empty;
			});
		}
	}
}
