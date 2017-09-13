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
        public string Name { get; internal set; }
        /// <summary>
        /// Activity - simple representation
        /// </summary>
        [JsonProperty("activity")]
        public ActivityMeta Activity { get; internal set; }
        /// <summary>
        /// Athlete - simple representation
        /// </summary>
        [JsonProperty("athlete")]
        public AthleteMeta Athlete { get; internal set; }
        /// <summary>
        /// Elapsed time in seconds
        /// </summary>
        [JsonProperty("elapsed_time")]
        public int ElapsedTime { get; internal set; }
        /// <summary>
        /// Moving time in seconds
        /// </summary>
        [JsonProperty("moving_time")]
        public int MovingTime { get; internal set; }
        [JsonProperty("start_date")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime StartDate { get; internal set; }
        [JsonProperty("start_date_local")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime StartDateLocal { get; internal set; }

        /// <summary>
        /// meters, the length of the effort as described by the activity, this may be different than the length of the segment
        /// </summary>
        [JsonProperty("distance")]
        public float Distance { get; internal set; }
        /// <summary>
        /// the activity stream index of the start of this effort
        /// </summary>
        [JsonProperty("start_index")]
        public int StartIndex { get; internal set; }

        /// <summary>
        /// the activity stream index of the end of this effort
        /// </summary>
        [JsonProperty("end_index")]
        public int EndIndex { get; internal set; }

        /// <summary>
        /// RPM, missing if not provided
        /// </summary>
        [JsonProperty("average_cadence")]
        public float AvgCadence { get; internal set; }

        /// <summary>
        /// Average watts - rides only
        /// </summary>
        [JsonProperty("average_watts")]
        public float AvgWatts { get; internal set; }
        
        /// <summary>
        /// True if watts from a real power meter
        /// </summary>
        [JsonProperty("device_watts")]
        public bool DeviceWatts { get; internal set; }

        /// <summary>
        /// BPM, missing if not provided
        /// </summary>
        [JsonProperty("average_heartrate")]
        public float AvgHeartRate { get; internal set; }
        /// <summary>
        /// BPM, missing if not provided
        /// </summary>
        [JsonProperty("max_heartrate")]
        public float MaxHeartRate { get; internal set; }
        /// <summary>
        /// summary representation of the covered segment
        /// </summary>
        [JsonProperty("segment")]
        public SegmentSummary Segment { get; internal set; }
        /// <summary>
        /// 1-10 rank on segment at time of upload
        /// </summary>
        [JsonProperty("kom_rank")]
        public int? KomRank { get; internal set; }
        /// <summary>
        /// 1-3 personal record on segment at time of upload
        /// </summary>
        [JsonProperty("pr_rank")]
        public int? PersonalRank { get; internal set; }
        /// <summary>
        /// indicates a hidden/non-important effort when returned as part of an activity, value may change over time, see retrieve an activity for more details    
        /// </summary>
        [JsonProperty("hidden")]
        public bool Hidden { get; internal set; }
    }
}
