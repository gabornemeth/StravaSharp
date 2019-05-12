using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    public interface ILeaderboardEntry
    {
        /// <summary>
        /// Name of the athlete
        /// </summary>
        string AthleteName { get; }

        /// <summary>
        /// Id of the athlete
        /// </summary>
        int AthleteId { get; }

        /// <summary>
        /// Gender of the athlete
        /// </summary>
        string AthleteGender { get; }

        /// <summary>
        /// Average heart rate [bpm]
        /// </summary>
        float? AvgHeartRate { get; }

        /// <summary>
        /// Average power [watts]
        /// </summary>
        float? AvgWatts { get; }
        /// <summary>
        /// Distance in meters
        /// </summary>
        float Distance { get; }
        /// <summary>
        /// Elapsed time in seconds
        /// </summary>
        int ElapsedTime { get; }
        /// <summary>
        /// Moving time in seconds
        /// </summary>
        int MovingTime { get; }

        /// <summary>
        /// Start date in UTC
        /// </summary>
        DateTime StartDate { get; }

        /// <summary>
        /// Start date in local timezone
        /// </summary>
        DateTime StartDateLocal { get; }

        /// <summary>
        /// Id of the activity where this effort belongs to
        /// </summary>
        long ActivityId { get; }

        /// <summary>
        /// Id of the effort
        /// </summary>
        long EffortId { get; }

        /// <summary>
        /// Current rank
        /// </summary>
        int Rank { get; }

        /// <summary>
        /// Url of the athlete's profile picture 
        /// </summary>
        string AthleteProfile { get; }

    }
}
