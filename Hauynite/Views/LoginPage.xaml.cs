using System;
using System.Reactive;
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
			         .Subscribe(result => {
				System.Diagnostics.Debug.WriteLine(result.ToString());
			});
		}
	}
}
