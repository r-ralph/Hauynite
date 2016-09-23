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

			viewModel.GetOwnNameAsync()
					 .Subscribe((name) =>
			{
				Title = name;
			}, (exception) =>
			{
				System.Diagnostics.Debug.WriteLine("Error: " + exception);
			});
		}
	}
}
