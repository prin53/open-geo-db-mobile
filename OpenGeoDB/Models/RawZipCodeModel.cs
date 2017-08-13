namespace OpenGeoDB.Models
{
    /// <summary>
    /// Represents raw zip code, with city name inside.
    /// </summary>
    public class RawZipCodeModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the city name (ORT).
        /// </summary>
        /// <value>The city identifier.</value>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets the zip code (PLZ).
        /// </summary>
        /// <value>The zip.</value>
        public int Zip { get; set; }

        /// <summary>
        /// Gets or sets the latitude of zip code area.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude  of zip code area.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude { get; set; }
    }
}
