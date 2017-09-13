//
// Extensions.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using RestSharp.Portable;
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

    internal static class RequestExtensions
    {
        public static void AddPaging(this IRestRequest request, int page = 0, int itemsPerPage = 0)
        {
            if (page != 0)
                request.AddParameter("page", page);
            if (itemsPerPage != 0)
                request.AddParameter("per_page", itemsPerPage);
        }
    }
}
