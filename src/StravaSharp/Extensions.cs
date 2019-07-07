//
// Extensions.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//    MathsHelpers (C) 2019 David Christensen
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
        private static readonly DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

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
            {
                request.AddParameter("page", page);
            }

            if (itemsPerPage != 0)
            {
                request.AddParameter("per_page", itemsPerPage);
            }
        }
    }
    public static class MathsHelpers
    {
        const float cEarthMeanRadiusMetres = 6371000;
        const float cPi = (float)Math.PI;
        public static float DegreeBearing(this LatLng from, LatLng to)
        {
            return DegreeBearing(from.Latitude, from.Longitude, to.Latitude, to.Longitude);
        }
        public static float DegreeBearing(float lat1, float lon1, float lat2, float lon2)
        {
            if (lat1 == 360 || lat2 == 360)
            {
                return 360; // "No data" indicator
            }
            //const float R = 6371; //earth’s radius (mean radius = 6,371km)
            var dLon = ToRad(lon2 - lon1);
            var dPhi = Math.Log(
                Math.Tan(ToRad(lat2) / 2 + Math.PI / 4) / Math.Tan(ToRad(lat1) / 2 + Math.PI / 4));
            if (Math.Abs(dLon) > Math.PI)
            {
                dLon = dLon > 0 ? -(2 * cPi - dLon) : (2 * cPi + dLon);
            }

            return ToBearing((float)Math.Atan2(dLon, dPhi));
        }

        public static float DistanceMetres(this LatLng from, LatLng to)
        {
            return (float)
              Math.Acos(Math.Cos(ToRad(90 - from.Latitude)) * Math.Cos(ToRad(90 - to.Latitude)) +
                        Math.Sin(ToRad(90 - from.Latitude)) * Math.Sin(ToRad(90 - to.Latitude)) *
                        Math.Cos(ToRad(from.Longitude - to.Longitude))) * cEarthMeanRadiusMetres;
        }

        public static LatLng MoveMetresDegrees(this LatLng from, float metres, float bearingInDegrees)
        {
            return MoveMetresRadians(from, metres, ToRad(bearingInDegrees));
        }
        public static LatLng MoveMetresRadians(this LatLng from, float metres, float bearingInRadians)
        {
            double lat2 = Math.Asin(Math.Sin(from.Latitude) * Math.Cos(metres / cEarthMeanRadiusMetres) + Math.Cos(from.Latitude) * Math.Sin(metres / cEarthMeanRadiusMetres) * Math.Cos(bearingInRadians));
            double lng2 = from.Longitude + Math.Atan2(
                Math.Sin(bearingInRadians) * Math.Sin(metres / cEarthMeanRadiusMetres) * Math.Cos(from.Latitude),
                Math.Cos(metres / cEarthMeanRadiusMetres) - Math.Sin(from.Latitude) * Math.Sin(lat2)
                );
            return new LatLng { Latitude = (float)lat2, Longitude = (float)lng2 };
        }

        private static float ToRad(float degrees)
        {
            return degrees * (cPi / 180);
        }

        private static float ToDegrees(float radians)
        {
            return radians * 180 / cPi;
        }

        private static float ToBearing(float radians)
        {
            // convert radians to degrees (as bearing: 0...360)
            return (ToDegrees(radians) + 360) % 360;
        }
    }
}
