using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using OpenGeoDB.Models;
using SQLite;

namespace OpenGeoDB.Services.Data
{
    public class SQLiteDataStore : IDataStore
    {
        private const string _databaseName = "OpenGeoDB.db";

        private readonly object _locker = new object();

        public SQLiteDataStore(ISQLiteConnectionFactory connectionFactory)
        {
            ConnectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));

            Connection = ConnectionFactory.CreateConnection(_databaseName);

            Initialize();
        }

        protected ISQLiteConnectionFactory ConnectionFactory { get; }

        protected SQLiteConnection Connection { get; }

        private void Initialize()
        {
            Connection.CreateTable<UpdateModel>();
            Connection.CreateTable<CityModel>();
            Connection.CreateTable<ZipCodeModel>();
        }

        public void ClearAll<TModel>() where TModel : class, new()
        {
            lock (_locker)
            {
                Connection.DeleteAll<TModel>();
            }
        }

        public void InsertAll<TModel>(IEnumerable<TModel> models) where TModel : class, new()
        {
            lock (_locker)
            {
                Connection.InsertAll(models);
            }
        }

        public void UpdateAll<TModel>(IEnumerable<TModel> models) where TModel : class, new()
        {
            lock (_locker)
            {
                Connection.UpdateAll(models);
            }
        }

        public IQueryable<TModel> GetAll<TModel>() where TModel : class, new()
        {
            lock (_locker)
            {
                return Connection.Table<TModel>().AsQueryable();
            }
        }

        public TModel GetById<TModel, TIdType>(TIdType id) where TModel : class, new()
        {
            lock (_locker)
            {
                return Connection.Find<TModel>(id);
            }
        }

        public int Count<TModel>(Expression<Func<TModel, bool>> expression) where TModel : class, new()
        {
            lock (_locker)
            {
                if (expression == null)
                {
                    return Connection.Table<TModel>().Count();
                }
                else
                {
                    return Connection.Table<TModel>().Count(expression);
                }
            }
        }

        public TModel FirstOrDefault<TModel>(Expression<Func<TModel, bool>> expression) where TModel : class, new()
        {
            lock (_locker)
            {
                if (expression == null)
                {
                    return Connection.Table<TModel>().FirstOrDefault();
                }
                else
                {
                    return Connection.Table<TModel>().FirstOrDefault(expression);
                }
            }
        }
    }
}
