//
// Extensions.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using Newtonsoft.Json;
using RestSharp;
using System;
using System.Threading.Tasks;

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

        public static string ToIso8601DateTimeString(this DateTime dt)
        {
            const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";
            return dt.ToString(DefaultDateTimeFormat);
        }
    }

    internal static class RequestExtensions
    {
        public static void AddPaging(this RestRequest request, int page = 0, int itemsPerPage = 0)
        {
            if (page != 0)
                request.AddParameter("page", page);
            if (itemsPerPage != 0)
                request.AddParameter("per_page", itemsPerPage);
        }

        public static async Task<T> ExecuteForJson<T>(this IRestClient restClient, RestRequest request)
        {
            var response = await restClient.ExecuteAsync(request).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
