using System;
namespace Hauynite
{
	public class DataRepository
	{
		private ISQLite sqlite;

		private string userId;

		public DataRepository(ISQLite sqlite, string userId)
		{
			this.sqlite = sqlite;
			this.userId = userId;
		}
	}
}
