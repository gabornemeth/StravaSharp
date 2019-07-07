//
// SegmentEffort.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//
        
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StravaSharp
{
    /// <summary>
    /// Summary of a segment effort
    /// </summary>
    public class SegmentEffort : StravaObject<long>
    {
        /// <summary>
        /// Name of the effort
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Activity - simple representation
        /// </summary>
        [JsonProperty("activity")]
        public ActivityMeta Activity { get; set; }
        /// <summary>
        /// Athlete - simple representation
        /// </summary>
        [JsonProperty("athlete")]
        public AthleteMeta Athlete { get; set; }
        /// <summary>
        /// Elapsed time in seconds
        /// </summary>
        [JsonProperty("elapsed_time")]
        public int ElapsedTime { get; set; }
        /// <summary>
        /// Moving time in seconds
        /// </summary>
        [JsonProperty("moving_time")]
        public int MovingTime { get; set; }
        [JsonProperty("start_date")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime StartDate { get; set; }
        [JsonProperty("start_date_local")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime StartDateLocal { get; set; }

        /// <summary>
        /// meters, the length of the effort as described by the activity, this may be different than the length of the segment
        /// </summary>
        [JsonProperty("distance")]
        public float Distance { get; set; }
        /// <summary>
        /// the activity stream index of the start of this effort
        /// </summary>
        [JsonProperty("start_index")]
        public int StartIndex { get; set; }

        /// <summary>
        /// the activity stream index of the end of this effort
        /// </summary>
        [JsonProperty("end_index")]
        public int EndIndex { get; set; }

        /// <summary>
        /// RPM, missing if not provided
        /// </summary>
        [JsonProperty("average_cadence")]
        public float AvgCadence { get; set; }

        /// <summary>
        /// Average watts - rides only
        /// </summary>
        [JsonProperty("average_watts")]
        public float AvgWatts { get; set; }
        
        /// <summary>
        /// True if watts from a real power meter
        /// </summary>
        [JsonProperty("device_watts")]
        public bool DeviceWatts { get; set; }

        /// <summary>
        /// BPM, missing if not provided
        /// </summary>
        [JsonProperty("average_heartrate")]
        public float AvgHeartRate { get; set; }
        /// <summary>
        /// BPM, missing if not provided
        /// </summary>
        [JsonProperty("max_heartrate")]
        public float MaxHeartRate { get; set; }
        /// <summary>
        /// summary representation of the covered segment
        /// </summary>
        [JsonProperty("segment")]
        public SegmentSummary Segment { get; set; }
        /// <summary>
        /// 1-10 rank on segment at time of upload
        /// </summary>
        [JsonProperty("kom_rank")]
        public int? KomRank { get; set; }
        /// <summary>
        /// 1-3 personal record on segment at time of upload
        /// </summary>
        [JsonProperty("pr_rank")]
        public int? PersonalRank { get; set; }
        /// <summary>
        /// indicates a hidden/non-important effort when returned as part of an activity, value may change over time, see retrieve an activity for more details    
        /// </summary>
        [JsonProperty("hidden")]
        public bool Hidden { get; set; }
    }
}
