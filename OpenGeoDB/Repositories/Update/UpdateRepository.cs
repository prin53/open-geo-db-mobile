using System;
using OpenGeoDB.Models;
using OpenGeoDB.Services.Data;

namespace OpenGeoDB.Repositories.Update
{
    public class UpdateRepository : IUpdateRepository
    {
        public UpdateRepository(IDataStore dataStore)
        {
            DataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
        }

        protected IDataStore DataStore { get; }

        public DateTime Updated
        {
            get => GetUpdated();
            set => SetUpdated(value);
        }

        protected DateTime GetUpdated()
        {
            return DataStore.FirstOrDefault<UpdateModel>()?.Updated ?? DateTime.MinValue;
        }

        protected void SetUpdated(DateTime date)
        {
            var model = new UpdateModel { Updated = date };

            if (DataStore.FirstOrDefault<UpdateModel>() != null)
            {
                DataStore.Update(model);
            }
            else
            {
                DataStore.Insert(model);
            }
        }
    }
}
