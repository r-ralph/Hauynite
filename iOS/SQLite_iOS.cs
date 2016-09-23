using System;
using System.IO;
using Hauynite.iOS;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_iOS))]

namespace Hauynite.iOS
{
	public class SQLite_iOS : ISQLite
	{
		public SQLite.SQLiteConnection GetConnection(string userId)
		{
			var sqliteFilename = userId + ".db3";
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
			string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
			var path = Path.Combine(libraryPath, sqliteFilename);
			// Create the connection
			var conn = new SQLite.SQLiteConnection(path);
			// Return the database connection
			return conn;
		}
	}
}
