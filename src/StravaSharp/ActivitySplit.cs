using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    public class ActivitySplit
    {
        /// <summary>
        /// Distance [meters]
        /// </summary>
        [JsonProperty("distance")]
        public float Distance { get; internal set; }
        /// <summary>
        /// seconds
        /// </summary>
        [JsonProperty("elapsed_time")]
        public int ElapsedTime { get; internal set; }
        /// <summary>
        /// meters
        /// </summary>
        [JsonProperty("elevation_difference")]
        public float ElevationDifference { get; internal set; }
        /// <summary>
        /// Moving time [sec]
        /// </summary>
        [JsonProperty("moving_time")]
        public int MovingTime { get; internal set; }
        /// <summary>
        /// Moving time [sec]
        /// </summary>
        [JsonProperty("split")]
        public int Split { get; internal set; }
        /// <summary>
        /// Average speed [meters per second]
        /// </summary>
        [JsonProperty("average_speed")]
        public float AverageSpeed { get; internal set; }
        /// <summary>
        /// Pace zone
        /// </summary>
        [JsonProperty("pace_zone")]
        public int PaceZone { get; internal set; }
    }
}
