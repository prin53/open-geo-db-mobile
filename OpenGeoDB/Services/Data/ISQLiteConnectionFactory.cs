using SQLite;

namespace OpenGeoDB.Services.Data
{
    /// <summary>
    /// Factory that creates SQLite connection.
    /// </summary>
    public interface ISQLiteConnectionFactory
    {
        /// <summary>
        /// Creates the connection.
        /// </summary>
        /// <returns>The connection.</returns>
        /// <param name="name">Database name.</param>
        SQLiteConnection CreateConnection(string name);
    }
}
