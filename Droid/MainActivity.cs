using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using System.Collections.Generic;

namespace Hauynite.Droid
{
	[Activity(Label = "Hauynite.Droid",
	          Icon = "@drawable/icon",
	          Theme = "@style/MyTheme",
	          MainLauncher = true,
	          ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		internal FacebookClient client;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(savedInstanceState);
			client = new FacebookClient(this);
			client.Init();

			Xamarin.Forms.Forms.Init(this, savedInstanceState);

			LoadApplication(new Hauynite());
		}

		protected override void OnActivityResult(int requestCode, Android.App.Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			client.OnActivityResult(requestCode, (int)resultCode, data);
		}
	}
}
