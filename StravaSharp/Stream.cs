using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StravaSharp
{
	/// <summary>
	/// Stream type.
	/// </summary>
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

	/// <summary>
	/// Resolution of the stream
	/// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StreamResolution
    {
		/// <summary>
		/// Low resolution
		/// </summary>
        [EnumMember(Value = "low")]
        Low,
		/// <summary>
		/// Medium resolution
		/// </summary>
        [EnumMember(Value = "medium")]
        Medium,
		/// <summary>
		/// High resolution
		/// </summary>
        [EnumMember(Value = "high")]
        High
    }

	/// <summary>
	/// Type of series
	/// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SeriesType
    {
		/// <summary>
		/// Time based series
		/// </summary>
        [EnumMember(Value = "time")]
        Time,
		/// <summary>
		/// Distance based series
		/// </summary>
        [EnumMember(Value = "distance")]
        Distance
    }

    /// <summary>
    /// Activity stream
    /// </summary>
    public class Stream
    {
		/// <summary>
		/// Type of the stream
		/// </summary>
        [JsonProperty("type")]
        public StreamType Type { get; internal set; }

		/// <summary>
        /// Array of stream values
        /// </summary>
        [JsonProperty("data")]
        public object[] Data { get; internal set; }
        
		/// <summary>
        /// Series type used for down sampling, will be present even if not used
        /// </summary>
        [JsonProperty("series_type")]
        public SeriesType SeriesType { get; internal set; }
        
		/// <summary>
        /// Complete stream length
        /// </summary>
        [JsonProperty("original_size")]
        public int Originalsize { get; internal set; }
        
		/// <summary>
        /// Resolution of the stream
        /// </summary>
        [JsonProperty("resolution")]
        public StreamResolution Resolution { get; internal set; }
    }
}
