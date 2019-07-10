//
// Segment.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StravaSharp
{
    internal class SegmentCollection
    {
        [JsonProperty("segments")]
        public List<SegmentSummary> Segments { get; set; }
    }

    /// <summary>
    /// Summary info about a segment
    /// </summary>
    public class SegmentSummary : StravaObject<long>
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("activity_type")]
        public ActivityType ActivityType { get; set; }
        /// <summary>
        /// meters
        /// </summary>
        [JsonProperty("distance")]
        public float Distance { get; set; }
        /// <summary>
        /// percent
        /// </summary>
        [JsonProperty("average_grade")]
        public float AverageGrade { get; set; }
        /// <summary>
        /// percent
        /// </summary>
        [JsonProperty("maximum_grade")]
        public float MaximumGrade { get; set; }
        /// <summary>
        /// meters
        /// </summary>
        [JsonProperty("elevation_high")]
        public float ElevationHigh { get; set; }
        /// <summary>
        /// meters
        /// </summary>
        [JsonProperty("elevation_low")]
        public float ElevationLow { get; set; }
        [JsonProperty("start_latlng")]
        public LatLng StartLatLng { get; set; }
        [JsonProperty("end_latlng")]
        public LatLng EndLatLng { get; set; }
        /// <summary>
        /// [0, 5], lower is harder
        /// </summary>
        [JsonProperty("climb_category")]
        public int ClimbCategory { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("private")]
        public bool IsPrivate { get; set; }
        /// <summary>
        /// true if authenticated athlete has starred segment
        /// </summary>
        [JsonProperty("starred")]
        public bool Starred { get; set; }
    }

    /// <summary>
    /// Segment info
    /// </summary>
    public class Segment : SegmentSummary
    {
        [JsonProperty("created_at")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))]
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// meters
        /// </summary>
        [JsonProperty("total_elevation_gain")]
        public float TotalElevationGain { get; set; }

        /// <summary>
        /// includes a summary polyline
        /// </summary>
        [JsonProperty("map")]
        public Map Map { get; set; }

        /// <summary>
        /// number of attempts
        /// </summary>
        [JsonProperty("effort_count")]
        public int EffortCount { get; set; }
        /// <summary>
        /// number of unique athletes
        /// </summary>
        [JsonProperty("athlete_count")]
        public int AthleteCount { get; set; }
        [JsonProperty("hazardous")]
        public bool Hazardous { get; set; }
        /// <summary>
        /// number of stars on this segment
        /// </summary>
        [JsonProperty("star_count")]
        public int StarCount { get; set; }
    }
}
