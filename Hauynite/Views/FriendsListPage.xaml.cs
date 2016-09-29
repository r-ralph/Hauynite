using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Hauynite
{
	public partial class FriendsListPage : ContentPage
	{
		FriendsListViewModel viewModel;

		public FriendsListPage()
		{
			InitializeComponent();

			viewModel = new FriendsListViewModel();
			BindingContext = viewModel;

			viewModel.GetOwnNameAsync()
					 .Subscribe((name) =>
					 {
						 Title = name;
					 }, (exception) =>
					 {
						 System.Diagnostics.Debug.WriteLine("Error: " + exception);
					 });
		}

		public void OnClickMenuItem(object sender, EventArgs e)
		{
			//DisplayAlert("Seleted", ((ToolbarItem)sender).Name, "OK");
		}
	}
}
