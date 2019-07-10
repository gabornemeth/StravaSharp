using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace StravaSharp
{

    /// <summary>
    /// Simple activity representation
    /// </summary>
    public class ActivityMeta : StravaObject<long>
    {
    }

    /// <summary>
    /// Summary representation of an activity
    /// </summary>
    public class ActivitySummary : ActivityMeta
    {
        /// <summary>
        /// provided at upload
        /// </summary>
        [JsonProperty("external_id")]
        public string ExternalId { get; set; }

        [JsonProperty("upload_id", NullValueHandling = NullValueHandling.Ignore)]
        public long UploadId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// meta or summary representation of the athlete
        /// </summary>
        [JsonProperty("athlete")]
        public AthleteMeta Athlete { get; set; }

        /// <summary>
        /// Distance [meters]
        /// </summary>
        [JsonProperty("distance")]
        public float Distance { get; set; }
        /// <summary>
        /// Moving time [sec]
        /// </summary>
        [JsonProperty("moving_time")]
        public int MovingTime { get; set; }
        /// <summary>
        /// seconds
        /// </summary>
        [JsonProperty("elapsed_time")]
        public int ElapsedTime { get; set; }

        /// <summary>
        /// meters
        /// </summary>
        [JsonProperty("total_elevation_gain")]
        public float TotalElevationGain { get; set; }
        /// <summary>
        /// Activity type, ie.ride, run, swim, etc.
        /// </summary>
        [JsonProperty("type")]
        public ActivityType Type { get; set; }

		/// <summary>
		/// Starting date
		/// </summary>
		/// <value>The start date.</value>
        [JsonProperty("start_date")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime StartDate { get; set; }

		/// <summary>
		/// Starting time in local time zone
		/// </summary>
        [JsonProperty("start_date_local")]
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime StartDateLocal { get; set; }

        [JsonProperty("timezone")]
        public string TimeZone { get; set; }

        [JsonProperty("start_latlng")]
        public LatLng StartLatLng { get; set; }

        [JsonProperty("end_ltlng")]
        public LatLng EndLatLng { get; set; }

        [JsonProperty("achievement_count")]
        public int AchievementCount { get; set; }

        /// <summary>
        /// Number of kudos.
        /// </summary>
        [JsonProperty("kudos_count")]
        public int KudosCount { get; set; }

        /// <summary>
        /// Number of commen.ts
        /// </summary>
        [JsonProperty("comment_count")]
        public int CommentCount { get; set; }

        /// <summary>
        /// number of athletes taking part in this “group activity”. >= 1 
        /// </summary>
        [JsonProperty("athlete_count")]
        public int AthleteCount { get; set; }

        /// <summary>
        /// Number of Instagram photos
        /// </summary>
        [JsonProperty("photo_count")]
        public int PhotoCount { get; set; }

        /// <summary>
        /// Total number of photos(Instagram and Strava)
        /// </summary>
        [JsonProperty("total_photo_count")]
        public int TotalPhotoCount { get; set; }

        /// <summary>
        /// detailed representation of the route
        /// </summary>
        [JsonProperty("map")]
        public Map Map { get; set; }

        [JsonProperty("trainer")]
        public bool Trainer { get; set; }
        [JsonProperty("commute")]
        public bool Commute { get; set; }
        [JsonProperty("manual")]
        public bool Manual { get; set; }
        [JsonProperty("private")]
        public bool Private { get; set; }

        /// <summary>
        /// The name of the device used to record the activity.
        /// </summary>
        [JsonProperty("device_name")]
        public string DeviceName { get; set; }

        [JsonProperty("flagged")]
        public bool Flagged { get; set; }

        //workout_type: integer 
        // for runs only, 0 -> ‘default’, 1 -> ‘race’, 2 -> ‘long run’, 3 -> ‘intervals’ 
        //gear_id: string
        // corresponds to a bike or pair of shoes included in athlete details

        /// <summary>
        /// Average speed [meters per second]
        /// </summary>
        [JsonProperty("average_speed")]
        public float AverageSpeed { get; set; }
        /// <summary>
        /// Maximum speed [meters per second]
        /// </summary>
        [JsonProperty("max_speed")]
        public float MaxSpeed { get; set; }
        /// <summary>
        /// Average cadence [rpm]
        /// </summary>
        [JsonProperty("average_cadence")]
        public float AverageCadence { get; set; }

        /// <summary>
        /// degrees Celsius, if provided at upload
        /// </summary>
        [JsonProperty("average_temp")]
        public float AverageTemperature { get; set; }

        /// <summary>
        /// Average watts (rides only)
        /// </summary>
        [JsonProperty("average_watts")]
        public float AveragePower { get; set; }

        /// <summary>
        /// Maximum watts (rides only)
        /// </summary>
        [JsonProperty("max_watts")]
        public int MaxPower { get; set; }

        /// <summary>
        /// weighted_average_watts: integer rides with power meter data only
        /// similar to xPower or Normalized Power
        /// </summary>
        [JsonProperty("weighted_average_watts")]
        public int NormalizedPower { get; set; }

        /// <summary>
        /// kilojoules: float rides only
        /// uses estimated power if necessary
        /// </summary>
        [JsonProperty("kilojoules")]
        public float Kilojoules { get; set; }

        /// <summary>
        /// true if the watts are from a power meter, false if estimated
        /// </summary>
        [JsonProperty("device_watts")]
        public bool DeviceWatts { get; set; }

        /// <summary>
        /// average_heartrate: float only if recorded with heartrate
        /// average over moving portion
        /// </summary>
        [JsonProperty("average_heartrate")]
        public float AverageHeartRate { get; set; }
        /// <summary>
        /// max_heartrate: integer only if recorded with heartrate
        /// </summary>
        [JsonProperty("max_heartrate")]
        public float MaxHeartRate { get; set; }

        /// <summary>
        /// if the authenticated athlete has kudoed this activity
        /// </summary>
        [JsonProperty("has_kudoed")]
        public bool HasKudoed { get; set; }
    }

    /// <summary>
    /// Detailed activity representation
    /// </summary>
    public class Activity : ActivitySummary
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// kilocalories, uses kilojoules for rides and speed/pace for runs
        /// </summary>
        [JsonProperty("calories")]
        public float Calories { get; set; }

        //gear: object
        //gear summary

        /// <summary>
        /// A list of segment efforts.
        /// </summary>
        [JsonProperty("segment_efforts")]
        public List<SegmentEffort> SegmentEfforts { get; set; }

        //splits_metric: array of metric split summaries
        // running activities only

        //splits_standard: array of standard split summaries
        // running activities only

        //best_efforts: array of best effort summaries
        // running activities only

        //photos: object
        //photos summary
    }
}
