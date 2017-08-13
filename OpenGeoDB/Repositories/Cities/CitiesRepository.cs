using System.Collections.Generic;
using System.Linq;
using OpenGeoDB.Models;
using OpenGeoDB.Services.Data;

namespace OpenGeoDB.Repositories.Cities
{
    public class CitiesRepository : RepositoryBase<CityModel>, ICitiesRepository
    {
        public CitiesRepository(IDataStore dataStore) : base(dataStore)
        {
            /* Required constructor */
        }

        public override IEnumerable<CityModel> GetAll()
        {
            return DataStore.GetAll<CityModel>().OrderBy(item => item.Name).ToList();
        }
    }
}
