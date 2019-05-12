using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    public interface ILeaderboard
    {
        /// <summary>
        /// Total number of entries in the leaderboard
        /// </summary>
        int EntryCount { get; }
        /// <summary>
        /// Entries of the current fetch
        /// </summary>
        IReadOnlyList<ILeaderboardEntry> Entries { get; }

    }
}
