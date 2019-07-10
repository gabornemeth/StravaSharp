using System;
using Newtonsoft.Json;

namespace StravaSharp
{

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
