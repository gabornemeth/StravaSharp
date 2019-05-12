using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    public interface ISegment : ISegmentSummary
    {
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
        /// <summary>
        /// meters
        /// </summary>
        float TotalElevationGain { get; }

        /// <summary>
        /// includes a summary polyline
        /// </summary>
        IMap Map { get; }

        /// <summary>
        /// number of attempts
        /// </summary>
        int EffortCount { get; }
        /// <summary>
        /// number of unique athletes
        /// </summary>
        int AthleteCount { get; }
        bool Hazardous { get; }
        /// <summary>
        /// number of stars on this segment
        /// </summary>
        int StarCount { get; }

    }
}
