using System.Collections.Generic;
using OpenGeoDB.Models;

namespace OpenGeoDB.Repositories.ZipCodes
{
    /// <summary>
    /// Contains methods to get zip codes.
    /// </summary>
    public interface IZipCodesRepository : IRepository<ZipCodeModel>
    {
        /// <summary>
        /// Gets the zip codes count.
        /// </summary>
        /// <returns>The zip codes count.</returns>
        /// <param name="cityId">City identifier.</param>
        int GetZipCodesCount(int cityId);

        /// <summary>
        /// Gets first zip code related to city.
        /// </summary>
        /// <returns>The zip code for city.</returns>
        /// <param name="cityId">City identifier.</param>
        ZipCodeModel GetFirstForCity(int cityId);

        /// <summary>
        /// Gets zip codes related to city.
        /// </summary>
        /// <returns>The all for city.</returns>
        /// <param name="cityId">City identifier.</param>
        IEnumerable<ZipCodeModel> GetAllForCity(int cityId);

        /// <summary>
        /// Gets the nearby zip codes.
        /// </summary>
        /// <returns>The nearby async.</returns>
        /// <param name="id">Identifier.</param>
        /// <param name="count">Count.</param>
        IEnumerable<ZipCodeModel> GetNearby(int id, int count);
    }
}
