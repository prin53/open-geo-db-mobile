using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OpenGeoDB.Models;
using OpenGeoDB.Repositories.Cities;
using OpenGeoDB.Repositories.Update;
using OpenGeoDB.Repositories.ZipCodes;
using OpenGeoDB.Services.Data;
using MvvmCross.Platform;

namespace OpenGeoDB.Services.Update
{
    public class UpdateService : IUpdateService
    {
        private const string Tag = nameof(UpdateService);

        public UpdateService(
            IDataProvider dataProvider,
            IUpdateRepository updateRepository,
            ICitiesRepository citiesRepository,
            IZipCodesRepository zipCodesRepository
        )
        {
            DataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
            UpdateRepository = updateRepository ?? throw new ArgumentNullException(nameof(updateRepository));
            CitiesRepository = citiesRepository ?? throw new ArgumentNullException(nameof(citiesRepository));
            ZipCodesRepository = zipCodesRepository ?? throw new ArgumentNullException(nameof(zipCodesRepository));
        }

        protected IDataProvider DataProvider { get; }

        protected IUpdateRepository UpdateRepository { get; }

        protected ICitiesRepository CitiesRepository { get; }

        protected IZipCodesRepository ZipCodesRepository { get; }

        public bool Outdated => IsOutdated();

        protected void ClearData()
        {
            CitiesRepository.Clear();
            ZipCodesRepository.Clear();
        }

        protected async Task<IList<IGrouping<string, RawZipCodeModel>>> LoadZipCodesGroupsAsync(CancellationToken cancellationToken)
        {
            var data = await DataProvider.LoadAsync(cancellationToken);

            return data.GroupBy(item => item.CityName).ToList();
        }

        protected IList<CityModel> GetCities(IList<IGrouping<string, RawZipCodeModel>> zipCodesGroupping)
        {
            return zipCodesGroupping.Select(item => new CityModel { Name = item.Key }).ToList();
        }

        protected IEnumerable<ZipCodeModel> GetZipCodes(IList<IGrouping<string, RawZipCodeModel>> zipCodesGroupping, IList<CityModel> cities)
        {
            var zipCodes = new List<ZipCodeModel>();

            for (int i = 0; i < zipCodesGroupping.Count; i++)
            {
                var city = zipCodesGroupping[i];

                var cityId = cities[i].Id;

                foreach (var zip in city)
                {
                    zipCodes.Add(new ZipCodeModel
                    {
                        Id = zip.Id,
                        CityId = cityId,
                        Zip = zip.Zip,
                        Latitude = zip.Latitude,
                        Longitude = zip.Longitude
                    });
                }
            }

            return zipCodes;
        }

        protected bool IsOutdated()
        {
            if (Configuration.ShouldForceUpdate)
            {
                return true;
            }

            return UpdateRepository.Updated == DateTime.MinValue;
        }

        public async Task UpdateAsync(CancellationToken cancellationToken)
        {
            Mvx.TaggedTrace(Tag, "Update requested");

            if (!Outdated)
            {
                Mvx.TaggedTrace(Tag, "All data is up to date, update not required");

                return;
            }

            ClearData();

            var zipCodesGroupping = await LoadZipCodesGroupsAsync(cancellationToken);

            var cities = GetCities(zipCodesGroupping);

            CitiesRepository.AddAll(cities);

            var zipCodes = GetZipCodes(zipCodesGroupping, cities);

            ZipCodesRepository.AddAll(zipCodes);

            UpdateRepository.Updated = DateTime.UtcNow;

            Mvx.TaggedTrace(Tag, "Update completed");
        }
    }
}
