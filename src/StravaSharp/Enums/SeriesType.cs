using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StravaSharp
{
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
}
