//
// TestHelper.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015-2023, Gabor Nemeth
//

using Newtonsoft.Json.Linq;
using StravaSharp.OAuth2Client;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    public static class TestHelper
    {
        private static TestAuthenticator _authenticator;

        public static async Task<TestAuthenticator> GetAuthenticator()
        {
            if (_authenticator == null)
            {
                _authenticator = new TestAuthenticator();
                await _authenticator.Update();
            }
            return _authenticator;
        }

        public static async Task<Client> CreateStravaClient()
        {
            var authenticator = await GetAuthenticator();
            return new Client(authenticator);
        }

        public static Client CreateFakeStravaClient()
        {
            var authenticator = new TestAuthenticator(Settings.AccessToken);
            return new FakeClient(authenticator);
        }
    }
}
