using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    internal static class Extensions
    {
        private static DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        public static long GetSecondsSinceUnixEpoch(this DateTime dt)
        {
            return Convert.ToInt64(dt.ToUniversalTime().Subtract(epoch).TotalSeconds);
        }
    }
}
