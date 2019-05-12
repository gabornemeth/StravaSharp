using System;

namespace StravaSharp
{
    public interface IActivitySummary : IActivityMeta
    {
        /// <summary>
        /// provided at upload
        /// </summary>
        string ExternalId { get; }

        long UploadId { get; }

        string Name { get; }

        /// <summary>
        /// meta or summary representation of the athlete
        /// </summary>
        IAthleteMeta Athlete { get; }
        /// <summary>
        /// Distance [meters]
        /// </summary>
        float Distance { get; }
        /// <summary>
        /// Moving time [sec]
        /// </summary>
        int MovingTime { get; }
        /// <summary>
        /// seconds
        /// </summary>
        int ElapsedTime { get; }

        /// <summary>
        /// meters
        /// </summary>
        float TotalElevationGain { get; }
        /// <summary>
        /// Activity type, ie.ride, run, swim, etc.
        /// </summary>
        ActivityType Type { get; }

        /// <summary>
        /// Starting date
        /// </summary>
        /// <value>The start date.</value>
        DateTime StartDate { get; }

        /// <summary>
        /// Starting time in local time zone
        /// </summary>
        DateTime StartDateLocal { get; }

        string TimeZone { get; }

        LatLng StartLatLng { get; }

        LatLng EndLatLng { get; }

        int AchievementCount { get; }

        /// <summary>
        /// Number of kudos.
        /// </summary>
        int KudosCount { get; }

        /// <summary>
        /// Number of commen.ts
        /// </summary>
        int CommentCount { get; }

        /// <summary>
        /// number of athletes taking part in this “group activity”. >= 1 
        /// </summary>
        int AthleteCount { get; }

        /// <summary>
        /// Number of Instagram photos
        /// </summary>
        int PhotoCount { get; }

        /// <summary>
        /// Total number of photos(Instagram and Strava)
        /// </summary>
        int TotalPhotoCount { get; }

        /// <summary>
        /// detailed representation of the route
        /// </summary>
        IMap Map { get; }

        bool Trainer { get; }
        bool Commute { get; }
        bool Manual { get; }
        bool Private { get; }

        /// <summary>
        /// The name of the device used to record the activity.
        /// </summary>
        string DeviceName { get; }

        bool Flagged { get; }

        //workout_type: integer 
        // for runs only, 0 -> ‘default’, 1 -> ‘race’, 2 -> ‘long run’, 3 -> ‘intervals’ 
        //gear_id: string
        // corresponds to a bike or pair of shoes included in athlete details

        /// <summary>
        /// Average speed [meters per second]
        /// </summary>
        float AverageSpeed { get; }
        /// <summary>
        /// Maximum speed [meters per second]
        /// </summary>
        float MaxSpeed { get; }
        /// <summary>
        /// Average cadence [rpm]
        /// </summary>
        float AverageCadence { get; }

        /// <summary>
        /// degrees Celsius, if provided at upload
        /// </summary>
        float AverageTemperature { get; }

        /// <summary>
        /// Average watts (rides only)
        /// </summary>
        float AveragePower { get; }

        /// <summary>
        /// Maximum watts (rides only)
        /// </summary>
        int MaxPower { get; }

        /// <summary>
        /// weighted_average_watts: integer rides with power meter data only
        /// similar to xPower or Normalized Power
        /// </summary>
        int NormalizedPower { get; }

        /// <summary>
        /// kilojoules: float rides only
        /// uses estimated power if necessary
        /// </summary>
        float Kilojoules { get; }

        /// <summary>
        /// true if the watts are from a power meter, false if estimated
        /// </summary>
        bool DeviceWatts { get; }

        /// <summary>
        /// average_heartrate: float only if recorded with heartrate
        /// average over moving portion
        /// </summary>
        float AverageHeartRate { get; }
        /// <summary>
        /// max_heartrate: integer only if recorded with heartrate
        /// </summary>
        float MaxHeartRate { get; }

        /// <summary>
        /// if the authenticated athlete has kudoed this activity
        /// </summary>
        bool HasKudoed { get; }

    }

}
