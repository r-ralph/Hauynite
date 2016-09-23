using System;
using Xamarin.Forms;

namespace Hauynite.Views
{
	public partial class LoginPage : ContentPage
	{
		LoginViewModel viewModel;

		public LoginPage()
		{
			InitializeComponent();

			viewModel = new LoginViewModel();
		}

		void OnLoginClicked(object sender, EventArgs args)
		{
			viewModel.LoginAsync()
					 .Subscribe(result =>
			{
				switch (result.Item1)
				{
					case Result.Success:
						(Application.Current as App).OnLogin(result.Item2);
						Application.Current.MainPage = new NavigationPage(new FriendsListPage());
						break;
					default:
						System.Diagnostics.Debug.WriteLine("Login: " + result);
						break;
				}
			});
		}
	}
}
