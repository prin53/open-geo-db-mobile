using System;
using System.Threading;
using System.Threading.Tasks;

namespace OpenGeoDB.Services.Update
{
    /// <summary>
    /// Represents service that performs updates when needed.
    /// </summary>
    public interface IUpdateService
    {
        /// <summary>
        /// Gets a value indicating whether data is outdated and have to be updated.
        /// </summary>
        /// <value><c>true</c> if outdated; otherwise, <c>false</c>.</value>
        bool Outdated { get; }

        /// <summary>
        /// Updates the data.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        Task UpdateAsync(CancellationToken cancellationToken);
    }

    public static class UpdateServiceExtensions
    {
        /// <summary>
        /// Updates the data.
        /// </summary>
        public static Task UpdateAsync(this IUpdateService updateService)
        {
            if (updateService == null)
            {
                throw new ArgumentNullException(nameof(updateService));
            }

            return updateService.UpdateAsync(CancellationToken.None);
        }
    }
}
