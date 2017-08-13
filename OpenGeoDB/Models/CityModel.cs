using SQLite;

namespace OpenGeoDB.Models
{
    /// <summary>
    /// Represents city (ORT).
    /// </summary>
    [Table("City")]
    public class CityModel
    {
        /// <summary>
        /// Gets or sets the identifier of particular city.
        /// </summary>
        /// <value>The identifier.</value>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the city name (ORT).
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }
    }
}
