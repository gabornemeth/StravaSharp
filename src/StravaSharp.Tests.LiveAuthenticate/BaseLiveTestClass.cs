using NUnit.Framework;
using RestSharp.Portable;
using System.Net.Http;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    public class BaseLiveTestClass
    {
        private readonly static HttpClient httpClient = new HttpClient();
        private async Task<string> GetAccessToken()
        {
            var response = await httpClient.GetAsync("https://stravaapitest.azurewebsites.net/api/GetToken");
            string token = await response.Content.ReadAsStringAsync();
            return token;
        }

        private IAuthenticator StravaTestAthleteAuthenticator;

        protected IStravaClient StravaTestAthleteClient;
        [OneTimeSetUp]
        public async Task Setup()
        {
            StravaTestAthleteAuthenticator = new TestAuthenticator(await GetAccessToken());
            StravaTestAthleteClient = new StravaClient(StravaTestAthleteAuthenticator);
        }

    }
}
