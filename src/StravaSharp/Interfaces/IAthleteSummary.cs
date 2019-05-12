using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    public interface IAthleteSummary : IAthleteMeta
    {
        string FirstName { get; }
        string LastName { get; }
        /// <summary>
        /// URL to a 62x62 pixel profile picture
        /// </summary>
        string ProfileMedium { get; }
        /// <summary>
        /// URL to a 124x124 pixel profile picture
        /// </summary>
        string Profile { get; }
        string City { get; }
        string State { get; }
        string Country { get; }
        /// <summary>
        /// ‘M’, ‘F’ or null
        /// </summary>
        string Sex { get; }
        /// <summary>
        /// ‘pending’, ‘accepted’, ‘blocked’ or ‘null’ 
        /// the authenticated athlete’s following status of this athlete
        /// </summary>
        string Friend { get; }
        /// <summary>
        /// ‘pending’, ‘accepted’, ‘blocked’ or ‘null’ 
        /// this athlete’s following status of the authenticated athlete
        /// </summary>
        string Follower { get; }
        bool Premium { get; }
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }
    }
}
