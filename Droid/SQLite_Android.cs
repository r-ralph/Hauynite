﻿using System.IO;
using Hauynite.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]

namespace Hauynite.Droid
{
	public class SQLite_Android : ISQLite
	{
		public SQLite.SQLiteConnection GetConnection(string userId)
		{
			var sqliteFilename = userId + ".db3";
			string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
			var path = Path.Combine(documentsPath, sqliteFilename);
			// Create the connection
			var conn = new SQLite.SQLiteConnection(path);
			// Return the database connection
			return conn;
		}
	}
}
