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
    public struct LatLng
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
    }

    internal class LatLngConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LatLng);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var latlng = new LatLng();
            var idx = 0;
            while (reader.TokenType != JsonToken.EndArray)
            {
                reader.Read();
                if (reader.TokenType == JsonToken.Float)
                {
                    if (idx++ == 0)
                        latlng.Latitude = Convert.ToSingle(reader.Value);
                    else
                        latlng.Longitude = Convert.ToSingle(reader.Value);
                }
            }
            return latlng;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
