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
        public string FirstName { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        /// <summary>
        /// URL to a 62x62 pixel profile picture
        /// </summary>
        [JsonProperty("profile_medium")]
        public string ProfileMedium { get; set; }
        /// <summary>
        /// URL to a 124x124 pixel profile picture
        /// </summary>
        [JsonProperty("profile")]
        public string Profile { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        /// <summary>
        /// ‘M’, ‘F’ or null
        /// </summary>
        [JsonProperty("sex")]
        public string Sex { get; set; }
        /// <summary>
        /// ‘pending’, ‘accepted’, ‘blocked’ or ‘null’ 
        /// the authenticated athlete’s following status of this athlete
        /// </summary>
        [JsonProperty("friend")]
        public string Friend { get; set; }
        /// <summary>
        /// ‘pending’, ‘accepted’, ‘blocked’ or ‘null’ 
        /// this athlete’s following status of the authenticated athlete
        /// </summary>
        [JsonProperty("follower")]
        public string Follower { get; set; }
        [JsonProperty("premium")]
        public bool Premium { get; set; }
        [JsonProperty("created_at")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.IsoDateTimeConverter))]
        public DateTime UpdatedAt { get; set; }
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
        public int FollowerCount { get; set; }
        [JsonProperty("friend_count")]
        public int FriendCount { get; set; }
        [JsonProperty("mutual_friend_count")]
        public int MutualFriendCount { get; set; }
        /// <summary>
        /// athlete’s default sport type: 0=cyclist, 1=runner
        /// </summary>
        [JsonProperty("athlete_type")]
        public AthleteType AthleteType { get; set; }
        [JsonProperty("date_preference")]
        public string DatePreference { get; set; }
        /// <summary>
        /// ‘feet’ or ‘meters’
        /// </summary>
        [JsonProperty("measurement_preference")]
        public string MeasurementPreference { get; set; }
        [JsonProperty("ftp", NullValueHandling = NullValueHandling.Ignore)]
        public int Ftp { get; set; }
        /// <summary>
        /// kilograms
        /// </summary>
        [JsonProperty("weight")]
        public float Weight { get; set; }

        //clubs:	array of object 
        //array of summary representations of the athlete’s clubs
        //bikes:	array of object 
        //array of summary representations of the athlete’s bikes
        //shoes:	array of object 
        //array of summary representations of the athlete’s shoes
    }
}
