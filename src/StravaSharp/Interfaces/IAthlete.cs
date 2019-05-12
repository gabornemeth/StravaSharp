using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    public interface IAthlete : IAthleteSummary
    {
        int FollowerCount { get; }
        int FriendCount { get; }
        int MutualFriendCount { get; }
        /// <summary>
        /// athlete’s default sport type: 0=cyclist, 1=runner
        /// </summary>
        AthleteType AthleteType { get; }
        string DatePreference { get; }
        /// <summary>
        /// ‘feet’ or ‘meters’
        /// </summary>
        string MeasurementPreference { get; }
        int Ftp { get; }
        /// <summary>
        /// kilograms
        /// </summary>
        float Weight { get; }

        //clubs:	array of object 
        //array of summary representations of the athlete’s clubs
        //bikes:	array of object 
        //array of summary representations of the athlete’s bikes
        //shoes:	array of object 
        //array of summary representations of the athlete’s shoes
    }
}

