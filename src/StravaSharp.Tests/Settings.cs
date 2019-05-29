//
// Settings.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using System;

namespace StravaSharp.Tests
{
    /// <summary>
    /// Test settings
    /// </summary>
    static class Settings
    {
        public static string AccessToken
        {
            get
            {
                return "287a30961471bca32da2ff14347ab161527bfc3a";
                //return Environment.GetEnvironmentVariable("STRAVASHARP_ACCESS_TOKEN");
            }
        }

        /// <summary>
        /// Identifier of test club: Femat-ZKSE
        /// </summary>
        public static int ClubId
        {
            get
            {
                return 226000;
            }
        }
    }
}
