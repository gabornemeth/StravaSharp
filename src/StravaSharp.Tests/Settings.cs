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
        /// <summary>
        /// A subset of the tests use an access token from the settings (below) to authenticate
        /// You need to get this token from Strava OAuth and enter it below.  It will be time limited
        /// so there is not much point in checking it back in to GitHub.
        /// For persistent tests, use the LiveAuthenticate test project.
        /// To avoid test failures when the AccessToken here is invalid, this next flag is available
        /// If set, all tests based on this access token will just pass.
        /// </summary>
        public static bool SkipAsPassedAccessTokenTests => true;
        
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
