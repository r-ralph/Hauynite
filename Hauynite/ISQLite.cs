using SQLite;

namespace Hauynite
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection(string userId);
	}
}
