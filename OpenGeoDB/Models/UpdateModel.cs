using System;
using SQLite;

namespace OpenGeoDB.Models
{
    /// <summary>
    /// Represents last update entity.
    /// </summary>
    [Table("Update")]
    public class UpdateModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [PrimaryKey]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date when app was updated.
        /// </summary>
        /// <value>The updated.</value>
        public DateTime Updated { get; set; }
    }
}
