//
// Extensions.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//
        
using System;

namespace StravaSharp
{
    internal static class Extensions
    {
        /// <summary>
        /// UNIX epoch
        /// </summary>
        private static DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        public static long GetSecondsSinceUnixEpoch(this DateTime dt)
        {
            return Convert.ToInt64(dt.ToUniversalTime().Subtract(epoch).TotalSeconds);
        }
    }
}
