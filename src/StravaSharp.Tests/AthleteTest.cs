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
            var client = TestHelper.CreateStravaClient();
            var athlete = await client.Athletes.GetCurrent();
            Assert.NotNull(athlete);
            Assert.NotNull(athlete.FirstName);
            Assert.NotNull(athlete.LastName);
        }
        
        [Test]
        public async Task GetAthlete()
        {
            var client = TestHelper.CreateStravaClient();
            var athlete = await client.Athletes.Get(TestAthleteId);
            Assert.NotNull(athlete);
            Assert.AreEqual("Gabor", athlete.FirstName);
            Assert.AreEqual("Nemeth", athlete.LastName);
        }
    }
}
