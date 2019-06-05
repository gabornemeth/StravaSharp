//
// Athlete.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace StravaSharp
{
    /// <summary>
    /// Meta representation of an athlete
    /// </summary>
    public class AthleteMeta : StravaObject<int>
    {
    }

    /// <summary>
    /// Summary representation of an athlete
    /// </summary>
    public class AthleteSummary : AthleteMeta
    {
        [JsonProperty("firstname")]
        public string FirstName { get; internal set; }
        [JsonProperty("lastname")]
        public string LastName { get; internal set; }
        /// <summary>
        /// URL to a 62x62 pixel profile picture
        /// </summary>
        [JsonProperty("profile_medium")]
        public string ProfileMedium { get; internal set; }
        /// <summary>
        /// URL to a 124x124 pixel profile picture
        /// </summary>
        [JsonProperty("profile")]
        public string Profile { get; internal set; }
        [JsonProperty("city")]
        public string City { get; internal set; }
        [JsonProperty("state")]
        public string State { get; internal set; }
        [JsonProperty("country")]
        public string Country { get; internal set; }
        /// <summary>
        /// ‘M’, ‘F’ or null
        /// </summary>
        [JsonProperty("sex")]
        public string Sex { get; internal set; }
        /// <summary>
        /// ‘pending’, ‘accepted’, ‘blocked’ or ‘null’ 
        /// the authenticated athlete’s following status of this athlete
        /// </summary>
        [JsonProperty("friend")]
        public string Friend { get; internal set; }
        /// <summary>
        /// ‘pending’, ‘accepted’, ‘blocked’ or ‘null’ 
        /// this athlete’s following status of the authenticated athlete
        /// </summary>
        [JsonProperty("follower")]
        public string Follower { get; internal set; }
        [JsonProperty("premium")]
        public bool Premium { get; internal set; }
        [JsonProperty("created_at")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))]
        public DateTime CreatedAt { get; internal set; }
        [JsonProperty("updated_at")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))]
        public DateTime UpdatedAt { get; internal set; }
    }

    /// <summary>
    /// Default sport type of the athlete
    /// </summary>
    public enum AthleteType
    {
		/// <summary>
		/// Cyclist
		/// </summary>
        [EnumMember(Value = "0")]
        Cyclist,
		/// <summary>
		/// Runner
		/// </summary>
        [EnumMember(Value = "1")]
        Runner
    }

    /// <summary>
    /// Detailed info about an athlete
    /// </summary>
    public class Athlete : AthleteSummary
    {
        [JsonProperty("follower_count")]
        public int FollowerCount { get; internal set; }
        [JsonProperty("friend_count")]
        public int FriendCount { get; internal set; }
        [JsonProperty("mutual_friend_count")]
        public int MutualFriendCount { get; internal set; }
        /// <summary>
        /// athlete’s default sport type: 0=cyclist, 1=runner
        /// </summary>
        [JsonProperty("athlete_type")]
        public AthleteType AthleteType { get; internal set; }
        [JsonProperty("date_preference")]
        public string DatePreference { get; internal set; }
        /// <summary>
        /// ‘feet’ or ‘meters’
        /// </summary>
        [JsonProperty("measurement_preference")]
        public string MeasurementPreference { get; internal set; }
        [JsonProperty("ftp", NullValueHandling = NullValueHandling.Ignore)]
        public int Ftp { get; internal set; }
        /// <summary>
        /// kilograms
        /// </summary>
        [JsonProperty("weight")]
        public float Weight { get; internal set; }

        //clubs:	array of object 
        //array of summary representations of the athlete’s clubs
        //bikes:	array of object 
        //array of summary representations of the athlete’s bikes
        //shoes:	array of object 
        //array of summary representations of the athlete’s shoes
    }
}
