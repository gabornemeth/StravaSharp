using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StravaSharp
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StreamType
    {
        /// <summary>
        /// integer seconds
        /// </summary>
        [EnumMember(Value = "time")]
        Time,
        /// <summary>
        /// floats [latitude, longitude]
        /// </summary>
        [EnumMember(Value = "latlng")]
        LatLng,
        /// <summary>
        /// float meters
        /// </summary>
        [EnumMember(Value = "distance")]
        Distance,
        /// <summary>
        /// float meters
        /// </summary>
        [EnumMember(Value = "altitude")]
        Altitude,
        /// <summary>
        /// float meters per second
        /// </summary>
        [EnumMember(Value = "velocity_smooth")]
        VelocitySmooth,
        /// <summary>
        /// integer BPM
        /// </summary>
        [EnumMember(Value = "heartrate")]
        HeartRate,
        /// <summary>
        /// integer RPM
        /// </summary>
        [EnumMember(Value = "cadence")]
        Cadence,
        /// <summary>
        /// integer watts
        /// </summary>
        [EnumMember(Value = "watts")]
        Watts,
        /// <summary>
        /// integer degrees Celsius
        /// </summary>
        [EnumMember(Value = "temp")]
        Temperature,
        /// <summary>
        /// boolean
        /// </summary>
        [EnumMember(Value = "moving")]
        Moving,
        /// <summary>
        /// float percent
        /// </summary>
        [EnumMember(Value = "grade_smooth")]
        GradeSmooth
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum StreamResolution
    {
        [EnumMember(Value = "low")]
        Low,
        [EnumMember(Value = "medium")]
        Medium,
        [EnumMember(Value = "high")]
        High
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum SeriesType
    {
        [EnumMember(Value = "time")]
        Time,
        [EnumMember(Value = "distance")]
        Distance
    }

    /// <summary>
    /// Activity stream
    /// </summary>
    public class Stream
    {
        [JsonProperty("type")]
        public StreamType Type { get; internal set; }
        /// <summary>
        /// array of stream values
        /// </summary>
        [JsonProperty("data")]
        public object[] Data { get; internal set; }
        /// <summary>
        /// series type used for down sampling, will be present even if not used
        /// </summary>
        [JsonProperty("series_type")]
        public SeriesType SeriesType { get; internal set; }
        /// <summary>
        /// complete stream length
        /// </summary>
        [JsonProperty("original_size")]
        public int Originalsize { get; internal set; }
        /// <summary>
        /// ‘low’, ‘medium’ or ‘high’
        /// </summary>
        [JsonProperty("resolution")]
        public StreamResolution Resolution { get; internal set; }
    }
}
