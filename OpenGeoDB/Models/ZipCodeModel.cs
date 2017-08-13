using SQLite;

namespace OpenGeoDB.Models
{
    /// <summary>
    /// Represents zip code (PLZ) with additional information, like city and location.
    /// </summary>
    [Table("ZipCode")]
    public class ZipCodeModel
    {
        public ZipCodeModel()
        {
            Location = new LocationModel();
        }

        /// <summary>
        /// Gets or sets the identifier of zip code model.
        /// </summary>
        /// <value>The identifier.</value>
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the city identifier linked to this zip code model.
        /// </summary>
        /// <value>The city identifier.</value>
        public int CityId { get; set; }

        /// <summary>
        /// Gets or sets the zip code (PLZ).
        /// </summary>
        /// <value>The zip.</value>
        public int Zip { get; set; }

        /// <summary>
        /// Gets or sets the latitude of zip code area.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude
        {
            get => Location.Latitude;
            set => Location.Latitude = value;
        }

        /// <summary>
        /// Gets or sets the longitude  of zip code area.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude
        {
            get => Location.Longitude;
            set => Location.Longitude = value;
        }

        /// <summary>
        /// Gets the location holder.
        /// </summary>
        /// <value>The location.</value>
        [Ignore]
        public LocationModel Location { get; }
    }
}
