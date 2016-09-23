using Xamarin.Forms;
using Hauynite.Views;

namespace Hauynite
{
	public partial class Hauynite : Application
	{
		private DataRepository repository;

		public Hauynite()
		{
			InitializeComponent();

			MainPage = new LoginPage();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		public void OnLogin(string userId)
		{
			var sqlite = DependencyService.Get<ISQLite>();
			repository = new DataRepository(sqlite, userId);
		}

		public DataRepository GetRepository()
		{
			return repository;
		}
	}
}
