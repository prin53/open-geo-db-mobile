using System;

namespace OpenGeoDB.Repositories.Update
{
    /// <summary>
    /// Update repository used to store information regarding app updates.
    /// </summary>
    public interface IUpdateRepository
    {
        /// <summary>
        /// Gets or sets the update date.
        /// </summary>
        /// <value>The updated.</value>
        DateTime Updated { get; set; }
    }
}
