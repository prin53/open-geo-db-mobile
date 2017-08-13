using System;
using System.Collections.Generic;
using OpenGeoDB.Services.Data;

namespace OpenGeoDB.Repositories
{
    public abstract class RepositoryBase<TModel> : IRepository<TModel> where TModel : class, new()
    {
        protected RepositoryBase(IDataStore dataStore)
        {
            DataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
        }

        protected IDataStore DataStore { get; }

        public virtual void Clear()
        {
            DataStore.ClearAll<TModel>();
        }

        public virtual void AddAll(IEnumerable<TModel> models)
        {
            DataStore.InsertAll(models);
        }

        public virtual IEnumerable<TModel> GetAll()
        {
            return DataStore.GetAll<TModel>();
        }

        public virtual TModel GetById(int id)
        {
            return DataStore.GetById<TModel, int>(id);
        }
    }
}
