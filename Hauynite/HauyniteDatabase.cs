using System;
using SQLite;
using Xamarin.Forms;

namespace Hauynite
{
	public class HauyniteDatabase
	{
		SQLiteConnection database;

		public HauyniteDatabase(string userId)
		{
			database = DependencyService.Get<ISQLite>().GetConnection(userId);
		}
	}
}
