//
// Comment.cs
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
    /// Comment for an activity
    /// </summary>
    internal class Comment : StravaObject<long>, IComment
    {
        /// <summary>
        /// Identifier of the parent activity
        /// </summary>
        [JsonProperty("activity_id")]
        public long ActivityId { get; internal set; }
        /// <summary>
        /// The actual comment
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; internal set; }
        /// <summary>
        /// summary representation of the commenting athlete
        /// </summary>
        [JsonProperty("athlete")]
        public AthleteSummary Athlete { get; internal set; }

        IAthleteSummary IComment.Athlete => Athlete;
        /// <summary>
        /// Time of creation
        /// </summary>
        [JsonProperty("created_at")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime CreateTime { get; internal set; }
    }
}
