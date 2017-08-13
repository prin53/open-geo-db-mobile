using OpenGeoDB.Models;

namespace OpenGeoDB.Services.Location
{
    public interface ILocationService
    {
        /// <summary>
        /// Gets the distance in meters between two zip codes by its locations.
        /// </summary>
        /// <returns>The distance.</returns>
        /// <param name="first">First location.</param>
        /// <param name="second">Second location.</param>
        double GetDistance(LocationModel first, LocationModel second);
    }
}
