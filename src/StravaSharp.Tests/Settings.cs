//
// Settings.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using Microsoft.Extensions.Configuration;
using System;

namespace StravaSharp.Tests
{
    /// <summary>
    /// Test settings
    /// </summary>
    static class Settings
    {
        private readonly static IConfigurationRoot _configuration;

        public static string ClientId => _configuration.GetString(nameof(ClientId)) ?? Environment.GetEnvironmentVariable("STRAVASHARP_TEST_CLIENTID");

        public static string ClientSecret => _configuration.GetString(nameof(ClientSecret)) ?? Environment.GetEnvironmentVariable("STRAVASHARP_TEST_CLIENTSECRET");

        public static string AccessToken => _configuration.GetString(nameof(AccessToken)) ?? Environment.GetEnvironmentVariable("STRAVASHARP_TEST_ACCESSTOKEN");

        public static string RefreshToken => _configuration.GetString(nameof(RefreshToken)) ?? Environment.GetEnvironmentVariable("STRAVASHARP_TEST_REFRESHTOKEN");

        /// <summary>
        /// Identifier of test club: Femat-ZKSE
        /// </summary>
        public static int ClubId => 226000;

        static Settings()
        {
            _configuration = new ConfigurationBuilder()
                                 .AddUserSecrets<ActivityTest>()
                                 .Build();
        }
    }

    static class ConfigurationExtensions
    {
        public static string GetString(this IConfigurationRoot configuration, string key)
        {
            var section = configuration.GetSection(key);
            return section?.Value;
        }
    }
}
