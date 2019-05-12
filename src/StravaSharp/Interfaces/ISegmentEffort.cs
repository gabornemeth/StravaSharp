using System;

namespace StravaSharp
{
    public interface ISegmentEffort
    {
        long Id { get; }
        /// <summary>
        /// Name of the effort
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Activity - simple representation
        /// </summary>
        IActivityMeta Activity { get; }
        /// <summary>
        /// Athlete - simple representation
        /// </summary>
        IAthleteMeta Athlete { get; }
        /// <summary>
        /// Elapsed time in seconds
        /// </summary>
        int ElapsedTime { get; }
        /// <summary>
        /// Moving time in seconds
        /// </summary>
        int MovingTime { get; }
        DateTime StartDate { get; }
        DateTime StartDateLocal { get; }

        /// <summary>
        /// meters, the length of the effort as described by the activity, this may be different than the length of the segment
        /// </summary>
        float Distance { get; }
        /// <summary>
        /// the activity stream index of the start of this effort
        /// </summary>
        int StartIndex { get; }

        /// <summary>
        /// the activity stream index of the end of this effort
        /// </summary>
        int EndIndex { get; }

        /// <summary>
        /// RPM, missing if not provided
        /// </summary>
        float AvgCadence { get; }

        /// <summary>
        /// Average watts - rides only
        /// </summary>
        float AvgWatts { get; }

        /// <summary>
        /// True if watts from a real power meter
        /// </summary>
        bool DeviceWatts { get; }

        /// <summary>
        /// BPM, missing if not provided
        /// </summary>
        float AvgHeartRate { get; }
        /// <summary>
        /// BPM, missing if not provided
        /// </summary>
        float MaxHeartRate { get; }
        /// <summary>
        /// summary representation of the covered segment
        /// </summary>
        ISegmentSummary Segment { get; }
        /// <summary>
        /// 1-10 rank on segment at time of upload
        /// </summary>
        int? KomRank { get; }
        /// <summary>
        /// 1-3 personal record on segment at time of upload
        /// </summary>
        int? PersonalRank { get; }
        /// <summary>
        /// indicates a hidden/non-important effort when returned as part of an activity, value may change over time, see retrieve an activity for more details    
        /// </summary>
        bool Hidden { get; }
    }
}
