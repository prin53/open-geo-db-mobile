using System.Collections.Generic;
using System.Linq;

namespace OpenGeoDB.Extensions
{
    public static class EnumerableExtensions
    {
        public static int GetPositionByKey<T>(this IEnumerable<IGrouping<string, T>> groupings, string key)
        {
            if (groupings == null)
            {
                return -1;
            }

            var enumerator = groupings.GetEnumerator();

            for (var i = 0; ; i++)
            {
                if (!enumerator.MoveNext())
                {
                    return -1;
                }

                if (enumerator.Current == null && key == null)
                {
                    return i;
                }

                if (enumerator.Current.Key.Equals(key))
                {
                    return i;
                }
            }
        }
    }
}
