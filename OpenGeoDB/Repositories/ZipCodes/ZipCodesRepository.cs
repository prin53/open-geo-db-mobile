using System;
using System.Collections.Generic;
using System.Linq;
using OpenGeoDB.Models;
using OpenGeoDB.Services.Data;
using OpenGeoDB.Services.Location;
using OpenGeoDB.Utilities;

namespace OpenGeoDB.Repositories.ZipCodes
{
    public class ZipCodesRepository : RepositoryBase<ZipCodeModel>, IZipCodesRepository
    {
        public ZipCodesRepository(
            IDataStore dataStore,
            ILocationService locationService
        ) : base(dataStore)
        {
            LocationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
        }

        protected ILocationService LocationService { get; }

        public int GetZipCodesCount(int cityId)
        {
            return DataStore.Count<ZipCodeModel>(item => item.CityId == cityId);
        }

        public ZipCodeModel GetFirstForCity(int cityId)
        {
            return DataStore.FirstOrDefault<ZipCodeModel>(item => item.CityId == cityId);
        }

        public IEnumerable<ZipCodeModel> GetAllForCity(int cityId)
        {
            return DataStore.GetAll<ZipCodeModel>()
                            .Where(item => item.CityId == cityId)
                            .OrderBy(item => item.Zip)
                            .ToList();
        }

        public IEnumerable<ZipCodeModel> GetNearby(int id, int count)
        {
            var model = GetById(id);

            return DataStore.GetAll<ZipCodeModel>()
                            .Where(item => item.Id != id)
                            .OrderBy(item => item, new ZipCodeComparer(LocationService, model))
                            .Take(count)
                            .ToList();
        }
    }
}
