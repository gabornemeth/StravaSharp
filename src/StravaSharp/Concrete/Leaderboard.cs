//
// Leaderboard.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//
        
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StravaSharp
{
    /// <summary>
    /// Leaderboard of a segment
    /// </summary>
    public class Leaderboard
    {
        /// <summary>
        /// Total number of entries in the leaderboard
        /// </summary>
        [JsonProperty("entry_count")]
        public int EntryCount { get; internal set; }
        /// <summary>
        /// Entries of the current fetch
        /// </summary>
        [JsonProperty("entries")]
        public List<LeaderboardEntry> Entries { get; internal set; }
    }

    /// <summary>
    /// Leaderboard entry
    /// </summary>
    public class LeaderboardEntry
    {
        /// <summary>
        /// Name of the athlete
        /// </summary>
        [JsonProperty("athlete_name")]
        public string AthleteName { get; internal set; }

        /// <summary>
        /// Id of the athlete
        /// </summary>
        [JsonProperty("athlete_id")]
        public int AthleteId { get; internal set; }

        /// <summary>
        /// Gender of the athlete
        /// </summary>
        [JsonProperty("athlete_gender")]
        public string AthleteGender { get; internal set; }
        
        /// <summary>
        /// Average heart rate [bpm]
        /// </summary>
        [JsonProperty("average_hr")]
        public float? AvgHeartRate { get; internal set; }

        /// <summary>
        /// Average power [watts]
        /// </summary>
        [JsonProperty("average_watts")]
        public float? AvgWatts { get; internal set; }
        /// <summary>
        /// Distance in meters
        /// </summary>
        [JsonProperty("distance")]
        public float Distance { get; internal set; }
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
        
        /// <summary>
        /// Start date in UTC
        /// </summary>
        [JsonProperty("start_date")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime StartDate { get; internal set; }
        
        /// <summary>
        /// Start date in local timezone
        /// </summary>
        [JsonProperty("start_date_local")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime StartDateLocal { get; internal set; }
        
        /// <summary>
        /// Id of the activity where this effort belongs to
        /// </summary>
        [JsonProperty("activity_id")]
        public long ActivityId { get; internal set; }
        
        /// <summary>
        /// Id of the effort
        /// </summary>
        [JsonProperty("effort_id")]
        public long EffortId { get; internal set; }
        
        /// <summary>
        /// Current rank
        /// </summary>
        [JsonProperty("rank")]
        public int Rank { get; internal set; }

        /// <summary>
        /// Url of the athlete's profile picture 
        /// </summary>
        [JsonProperty("athlete_profile")]
        public string AthleteProfile { get; internal set; }
    }
}
