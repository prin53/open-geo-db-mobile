using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OpenGeoDB.Services.Data
{
    public interface IDataStore
    {
        void ClearAll<TModel>() where TModel : class, new();

        void InsertAll<TModel>(IEnumerable<TModel> models) where TModel : class, new();

        void UpdateAll<TModel>(IEnumerable<TModel> models) where TModel : class, new();

        IQueryable<TModel> GetAll<TModel>() where TModel : class, new();

        TModel GetById<TModel, TIdType>(TIdType id) where TModel : class, new();

        int Count<TModel>(Expression<Func<TModel, bool>> expression) where TModel : class, new();

        TModel FirstOrDefault<TModel>(Expression<Func<TModel, bool>> expression) where TModel : class, new();
    }

    public static class DataStoreExtensions
    {
        public static void Insert<TModel>(this IDataStore dataStore, TModel model) where TModel : class, new()
        {
            if (dataStore == null)
            {
                throw new ArgumentNullException(nameof(dataStore));
            }

            dataStore.InsertAll(new[] { model });
        }

        public static void Update<TModel>(this IDataStore dataStore, TModel model) where TModel : class, new()
        {
            if (dataStore == null)
            {
                throw new ArgumentNullException(nameof(dataStore));
            }

            dataStore.UpdateAll(new[] { model });
        }

        public static TModel FirstOrDefault<TModel>(this IDataStore dataStore) where TModel : class, new()
        {
            if (dataStore == null)
            {
                throw new ArgumentNullException(nameof(dataStore));
            }

            return dataStore.FirstOrDefault<TModel>(null);
        }
    }
}
