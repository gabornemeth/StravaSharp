//
// TestHelper.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//
        
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    public static class TestHelper
    {
        private readonly static HttpClient httpClient = new HttpClient();
        private static async Task<string> GetAccessToken()
        {
            var response = await httpClient.GetAsync("https://stravaapitest.azurewebsites.net/api/GetToken");
            string token = await response.Content.ReadAsStringAsync();
            return token;
        }
        public static async Task<IStravaClient> StravaClientFromSettings()
        {
            string accessToken = Settings.AccessToken;
            if (accessToken==null)
            {
                accessToken = await GetAccessToken();
            }
            var authenticator = new TestAuthenticator(accessToken);
            return new StravaClient(authenticator);
        }
    }
}
