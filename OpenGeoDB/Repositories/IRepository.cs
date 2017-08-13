using System.Collections.Generic;

namespace OpenGeoDB.Repositories
{
    /// <summary>
    /// Represents repository with default set of operations.
    /// </summary>
    public interface IRepository<TModel> where TModel : class, new()
    {
        /// <summary>
        /// Clears the data of this repository.
        /// </summary>
        void Clear();

        /// <summary>
        /// Adds models to this repository.
        /// </summary>
        /// <param name="models">Models.</param>
        void AddAll(IEnumerable<TModel> models);

        /// <summary>
        /// Gets all models of this repository.
        /// </summary>
        /// <returns>The models.</returns>
        IEnumerable<TModel> GetAll();

        /// <summary>
        /// Gets model by identifier.
        /// </summary>
        /// <returns>The by identifier.</returns>
        TModel GetById(int id);
    }
}
