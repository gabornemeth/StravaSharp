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
}
