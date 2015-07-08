// Base of this code is from:
// http://www.codeproject.com/Tips/312248/Google-Maps-Direction-API-V-Polyline-Decoder
//
using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    /// <summary>
    /// Polyline decoder for Google Maps API
    /// </summary>
    public static class PolylineDecoder
    {
        public static List<LatLng> DecodePolylinePoints(string encodedPoints)
        {
            if (string.IsNullOrEmpty(encodedPoints))
                return null;

            var poly = new List<LatLng>();
            char[] polylinechars = encodedPoints.ToCharArray();
            int index = 0;

            int currentLat = 0;
            int currentLng = 0;
            int next5bits;
            int sum;
            int shifter;

            while (index < polylinechars.Length)
            {
                // calculate next latitude
                sum = 0;
                shifter = 0;
                do
                {
                    next5bits = (int)polylinechars[index++] - 63;
                    sum |= (next5bits & 31) << shifter;
                    shifter += 5;
                } while (next5bits >= 32 && index < polylinechars.Length);

                if (index >= polylinechars.Length)
                    break;

                currentLat += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);

                //calculate next longitude
                sum = 0;
                shifter = 0;
                do
                {
                    next5bits = (int)polylinechars[index++] - 63;
                    sum |= (next5bits & 31) << shifter;
                    shifter += 5;
                } while (next5bits >= 32 && index < polylinechars.Length);

                if (index >= polylinechars.Length && next5bits >= 32)
                    break;

                currentLng += (sum & 1) == 1 ? ~(sum >> 1) : (sum >> 1);
                var p = new LatLng
                {
                    Latitude = Convert.ToSingle(currentLat) / 100000.0f,
                    Longitude = Convert.ToSingle(currentLng) / 100000.0f
                };
                poly.Add(p);
            }
            return poly;
        }
    }
}
