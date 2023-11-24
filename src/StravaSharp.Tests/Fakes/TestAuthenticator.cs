//
// TestAuthenticator.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2016-2023, Gabor Nemeth
//

using RestSharp;
using StravaSharp.OAuth2Client;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    /// <summary>
    /// Authenticator used for tests
    /// </summary>
    public class TestAuthenticator : RestSharp.Authenticators.AuthenticatorBase
    {
        public TestAuthenticator(string accessToken = null) : base(accessToken)
        {
        }

        protected override ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            return new ValueTask<Parameter>(new HeaderParameter("Authorization", "Bearer " + Token));
        }

        public async Task Update()
        {
            var config = new OAuth2ClientConfiguration
            {
                ClientId = Settings.ClientId,
                ClientSecret = Settings.ClientSecret,
                RedirectUri = "" // does not matter here
            };
            var client = new StravaClient(config);
            await client.UpdateAccessToken(Settings.RefreshToken);
            Token = client.AccessToken;
        }
    }
}
