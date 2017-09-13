//
// LatLng.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using System;
using Newtonsoft.Json;

namespace StravaSharp
{
    /// <summary>
    /// Geographical position
    /// </summary>
    [JsonConverter(typeof(LatLngConverter))]
    public class LatLng
    {
        /// <summary>
        /// WGS84 latitude
        /// </summary>
        public float Latitude { get; set; }
        /// <summary>
        /// WGS84 longitude
        /// </summary>
        public float Longitude { get; set; }

        public bool IsEmpty()
        {
            return Latitude.Equals(0.0f) && Longitude.Equals(0.0f);
        }

        public bool Equals(LatLng latLng)
        {
            return latLng.Latitude == Latitude && latLng.Longitude == Longitude;
        }
    }

    internal class LatLngConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LatLng);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            var result = serializer.Deserialize<float[]>(reader);
            return new LatLng
            {
                Latitude = result[0],
                Longitude = result[1]
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
