//
// Club.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2016, Gabor Nemeth
//

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Runtime.Serialization;

namespace StravaSharp
{
	/// <summary>
	/// Sport type.
	/// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SportType
    {
		/// <summary>
		/// Cycling
		/// </summary>
        [EnumMember(Value = "cycling")]
        Cycling,
		/// <summary>
		/// Running
		/// </summary>
        [EnumMember(Value = "running")]
        Running,
		/// <summary>
		/// Triathlon
		/// </summary>
        [EnumMember(Value = "triathlon")]
        Triathlon,
		/// <summary>
		/// Other sports
		/// </summary>
        [EnumMember(Value = "other")]
        Other
    }

	/// <summary>
	/// Summary info about a club.
	/// </summary>
    public class ClubSummary : StravaObject<int>
    {
		/// <summary>
		/// Name of the club
		/// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// URL to a 60x60 pixel profile picture
        /// </summary>
        [JsonProperty("profile_medium")]
        public string ProfileMedium { get; set; }

        /// <summary>
        /// URL to a 124x124 pixel profile picture
        /// </summary>
        [JsonProperty("profile")]
        public string Profile { get; set; }

        /// <summary>
        /// URL to a ~1185x580 pixel cover photo
        /// </summary>
        [JsonProperty("cover_photo")]
        public string CoverPhoto { get; set; }

        /// <summary>
        /// URL to a ~360x176 pixel cover photo
        /// </summary>
        [JsonProperty("cover_photo_small")]
        public string CoverPhotoSmall { get; set; }

        /// <summary>
        /// cycling, running, triathlon, other
        /// </summary>
        [JsonProperty("sport_type")]
        public SportType SportType { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("private")]
        public bool Private { get; set; }

        [JsonProperty("member_count")]
        public int MemberCount { get; set; }

        [JsonProperty("featured")]
        public bool Featured { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        /// <summary>
        /// vanity club URL slug
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    /// <summary>
    /// Detailed representation of a club
    /// </summary>
    public class Club : ClubSummary
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// casual_club, racing_team, shop, company, other
        /// </summary>
        [JsonProperty("club_type")]
        public string ClubType { get; set; }

        /// <summary>
        /// membership status of the requesting athlete 
        /// "member", "pending", null (not a member and have not requested join)
        /// </summary>
        [JsonProperty("membership")]
        public string Membership { get; set; }

        /// <summary>
        /// true only if the requesting athlete is a club admin
        /// </summary>
        [JsonProperty("admin")]
        public bool Admin { get; set; }

        /// <summary>
        /// true only if the requesting athlete is the club owner
        /// </summary>
        [JsonProperty("owner")]
        public bool Owner { get; set; }

        /// <summary>
        /// total number of members the authenticated user is currently following
        /// </summary>
        [JsonProperty("following_count")]
        public bool FollowingCount { get; set; }
    }
}
