using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StravaSharp
{
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
}
