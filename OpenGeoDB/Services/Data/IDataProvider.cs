using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OpenGeoDB.Models;

namespace OpenGeoDB.Services.Data
{
    /// <summary>
    /// Represents data provider. Can load raw data.
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Loads raw data async.
        /// </summary>
        /// <returns>The data.</returns>
        /// <param name="CancellationToken">Cancellation token.</param>
        Task<IEnumerable<RawZipCodeModel>> LoadAsync(CancellationToken CancellationToken);
    }

    public static class DataProviderExtensions
    {
        /// <summary>
        /// Loads raw data async.
        /// </summary>
        /// <returns>The data.</returns>
        public static Task<IEnumerable<RawZipCodeModel>> LoadAsync(this IDataProvider dataProvider)
        {
            if (dataProvider == null)
            {
                throw new ArgumentNullException(nameof(dataProvider));
            }

            return dataProvider.LoadAsync(CancellationToken.None);
        }
    }
}
