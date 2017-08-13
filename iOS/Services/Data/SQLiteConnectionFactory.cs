using System;
using System.IO;
using OpenGeoDB.Services.Data;
using SQLite;

namespace OpenGeoDB.iOS.Services.Data
{
    public class SQLiteConnectionFactory : ISQLiteConnectionFactory
    {
        public SQLiteConnection CreateConnection(string name)
        {
            return new SQLiteConnection(
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), name),
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex | SQLiteOpenFlags.Create
            );
        }
    }
}
