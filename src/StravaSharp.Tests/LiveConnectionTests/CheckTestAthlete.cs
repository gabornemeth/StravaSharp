using NUnit.Framework;
using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StravaSharp.Tests.LiveConnectionTests
{
    [TestFixture]
    public class CheckTestAthlete
    {
        private readonly static HttpClient httpClient = new HttpClient();
        private async Task<string> GetAccessToken()
        {
            var response= await httpClient.GetAsync("https://stravaapitest.azurewebsites.net/api/GetToken");
            string token= await response.Content.ReadAsStringAsync();
            return token;
        }

        private  IAuthenticator StravaTestAthleteAuthenticator;
        private IStravaClient StravaTestAthleteClient;
        private const long cAthleteId = 42170685;
        [OneTimeSetUp]
        public async Task Setup()
        {
            StravaTestAthleteAuthenticator = new TestAuthenticator(await GetAccessToken());
            StravaTestAthleteClient = new StravaClient(StravaTestAthleteAuthenticator);
        }

        [Test]
        public async Task CheckTestAthleteData()
        {
            IAthlete current =await StravaTestAthleteClient.Athletes.GetCurrent();
            Assert.AreEqual("Test", current.FirstName);
            Assert.AreEqual("Athlete", current.LastName);
            Assert.AreEqual(cAthleteId, current.Id);
            Assert.AreEqual("Cambridge", current.City);
        }

        [Test]
        public async Task CheckActivities()
        {
            IActivityClient activityClient = StravaTestAthleteClient.Activities;
            IReadOnlyList<IActivitySummary> activities = await activityClient.GetAthleteActivities();
            Assert.AreEqual(1, activities.Count);
            IActivitySummary act = activities[0];
            Assert.AreEqual(cAthleteId, act.Athlete.Id);
            Assert.AreEqual(true, act.Manual);
            Assert.AreEqual(2366117371, act.Id);
            Assert.AreEqual(3723, act.ElapsedTime);
        }
    }
}
