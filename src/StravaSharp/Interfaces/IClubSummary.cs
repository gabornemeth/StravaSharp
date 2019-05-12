using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    public interface IClubSummary
    {
        int Id { get; }
        /// <summary>
        /// Name of the club
        /// </summary>
        string Name { get; }

        /// <summary>
        /// URL to a 60x60 pixel profile picture
        /// </summary>
        string ProfileMedium { get; }

        /// <summary>
        /// URL to a 124x124 pixel profile picture
        /// </summary>
        string Profile { get; }

        /// <summary>
        /// URL to a ~1185x580 pixel cover photo
        /// </summary>
        string CoverPhoto { get; }

        /// <summary>
        /// URL to a ~360x176 pixel cover photo
        /// </summary>
        string CoverPhotoSmall { get; }

        /// <summary>
        /// cycling, running, triathlon, other
        /// </summary>
        SportType SportType { get; }

        string City { get; }
        string State { get; }
        string Country { get; }

        bool Private { get; }

        int MemberCount { get; }

        bool Featured { get; }

        bool Verified { get; }

        /// <summary>
        /// vanity club URL slug
        /// </summary>
        string Url { get; }

    }
}
