using System;
using System.Collections.Generic;
using System.Text;

namespace PampaDevs.Utils.Extensions
{
    public static class HashExtensions
    {
        public static int CombineHashCodes(this IEnumerable<object> objs)
        {
            unchecked
            {
                var hash = 17;
                foreach (var obj in objs) hash = hash * 23 + (obj?.GetHashCode() ?? 0);

                return hash;
            }
        }
    }
}
