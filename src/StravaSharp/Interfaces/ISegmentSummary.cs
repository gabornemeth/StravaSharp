using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    public interface ISegmentSummary
    {
        long Id { get; }
        string Name { get; }
        ActivityType ActivityType { get; }
        /// <summary>
        /// meters
        /// </summary>
        float Distance { get; }
        /// <summary>
        /// percent
        /// </summary>
        float AverageGrade { get; }
        /// <summary>
        /// percent
        /// </summary>
        float MaximumGrade { get; }
        /// <summary>
        /// meters
        /// </summary>
        float ElevationHigh { get; }
        /// <summary>
        /// meters
        /// </summary>
        float ElevationLow { get; }
        LatLng StartLatLng { get; }
        LatLng EndLatLng { get; }
        /// <summary>
        /// [0, 5], lower is harder
        /// </summary>
        int ClimbCategory { get; }
        string City { get; }
        string State { get; }
        string Country { get; }
        bool IsPrivate { get; }
        /// <summary>
        /// true if authenticated athlete has starred segment
        /// </summary>
        bool Starred { get; }

    }
}
