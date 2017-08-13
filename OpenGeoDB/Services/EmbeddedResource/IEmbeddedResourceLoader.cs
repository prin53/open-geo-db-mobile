using System.IO;

namespace OpenGeoDB.Services.EmbeddedResource
{
    /// <summary>
    /// Represents resource loader that loads embedded resources.
    /// </summary>
    public interface IEmbeddedResourceLoader
    {
        /// <summary>
        /// Gets the stream for resource.
        /// </summary>
        /// <returns>The stream.</returns>
        /// <param name="resourceName">Resource name.</param>
        Stream GetStream(string resourceName);
    }
}
