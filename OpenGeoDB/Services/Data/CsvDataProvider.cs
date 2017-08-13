using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using OpenGeoDB.Models;
using OpenGeoDB.Services.EmbeddedResource;

namespace OpenGeoDB.Services.Data
{
    public class CsvDataProvider : IDataProvider
    {
        private const int ColumnsCount = 5;
        private const string NewLine = "\t";
        private const string Header = "#";
        private const int IdIndex = 0;
        private const int ZipCodeIndex = 1;
        private const int LongitudeIndex = 2;
        private const int LatitudeIndex = 3;
        private const int CityIndex = 4;

        public CsvDataProvider(IEmbeddedResourceLoader embeddedResourceLoader)
        {
            EmbeddedResourceLoader = embeddedResourceLoader ?? throw new ArgumentNullException(nameof(embeddedResourceLoader));
        }

        protected IEmbeddedResourceLoader EmbeddedResourceLoader { get; }

        public Task<IEnumerable<RawZipCodeModel>> LoadAsync(CancellationToken CancellationToken)
        {
            return Task.Run(() => Load());
        }

        protected IEnumerable<RawZipCodeModel> Load()
        {
            using (var stream = EmbeddedResourceLoader.GetStream(Configuration.DataResourceName))
            {
                using (var reader = new StreamReader(stream))
                {
                    var result = new List<RawZipCodeModel>();

                    var line = string.Empty;

                    var headerPassed = false;

                    while ((line = reader.ReadLine()) != null)
                    {
                        var items = GetItems(line);

                        if (items.Count() < ColumnsCount)
                        {
                            continue;
                        }

                        if (!headerPassed && IsHeader(items))
                        {
                            headerPassed = true;

                            continue;
                        }

                        result.Add(ParseItems(items));
                    }

                    return result;
                }
            }
        }

        protected RawZipCodeModel ParseItems(string[] items)
        {
            int.TryParse(items[IdIndex], out int id);
            int.TryParse(items[ZipCodeIndex], out int zip);
            double.TryParse(items[LatitudeIndex], out double latitude);
            double.TryParse(items[LongitudeIndex], out double longitude);

            return new RawZipCodeModel
            {
                Id = id,
                CityName = items[CityIndex],
                Zip = zip,
                Latitude = latitude,
                Longitude = longitude
            };
        }

        protected string[] GetItems(string line)
        {
            return Regex.Split(line, NewLine);
        }

        protected bool IsHeader(string[] items)
        {
            return items.FirstOrDefault()?.StartsWith(Header, StringComparison.Ordinal) ?? false;
        }
    }
}
