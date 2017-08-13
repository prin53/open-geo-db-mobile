using System.IO;
using System.Reflection;

namespace OpenGeoDB.Services.EmbeddedResource
{
    public class EmbeddedResourceLoader : IEmbeddedResourceLoader
    {
        public Stream GetStream(string resourceName)
        {
            return typeof(EmbeddedResourceLoader).GetTypeInfo().Assembly.GetManifestResourceStream(resourceName);
        }
    }
}
