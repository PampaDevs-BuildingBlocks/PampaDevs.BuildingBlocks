using System;
using System.Diagnostics;

namespace PampaDevs.Utils.Helpers
{
    public static class DateTimeHelper
    {
        [DebuggerStepThrough]
        public static DateTime NewDateTime()
        {
            return DateTimeOffset.Now.UtcDateTime;
        }

        public static long ToUnixEpochDate(DateTime date)
        {
            var timespan = (date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero));
            
            return (long)Math.Round(timespan.TotalSeconds);
        }
            
    }
}
