using NUnit.Framework;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    /// <summary>
    /// Athlete tests
    /// Access token is read-write for gabor.nemeth.dev@gmail.com (Extrava Test Account)
    /// </summary>
    [TestFixture]
    public class AthleteTest
    {
        private const int TestAthleteId = 4076458; // gabor.nemeth1982@gmail.com

        [Test]
        public async Task GetCurrentAthlete()
        {
            if (!Settings.SkipAsPassedAccessTokenTests)
            {
                var client = TestHelper.StravaClientFromSettings();
                var athlete = await client.Athletes.GetCurrent();
                Assert.NotNull(athlete);
                Assert.AreEqual("Extrava", athlete.FirstName);
            }
        }

        [Test]
        public async Task GetAthlete()
        {
            if (!Settings.SkipAsPassedAccessTokenTests)
            {
                var client = TestHelper.StravaClientFromSettings();
                var athlete = await client.Athletes.Get(6632444);
                Assert.NotNull(athlete);
                Assert.AreEqual("Extrava", athlete.FirstName);
            }
        }
    }
}
