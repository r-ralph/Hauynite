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
				switch (result)
				{
					case Result.Success:
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
