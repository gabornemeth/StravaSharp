using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    [TestFixture]
    public class FetchTest
    {
        [Test]
        public async Task GetActivities()
        {
            var auth = new StaticAuthenticator(Settings.AccessToken);

            //var config = new RestSharp.Portable.Authenticators.OAuth2.Configuration.RuntimeClientConfiguration();
            //config.IsEnabled = false;
            //config.ClientId = clientId;
            //config.ClientSecret = clientSecret;
            //config.RedirectUri = redirectUrl.AbsoluteUri;
            //config.Scope = scope;
            //var client = new StravaClient(new RequestFactory(), config);

            var client = new Client(null, auth);
            var activities = await client.GetActivities();
            Assert.True(activities.Count > 0);
        }
    }
}
