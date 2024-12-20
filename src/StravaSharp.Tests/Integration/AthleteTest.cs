using NUnit.Framework;
using System.Threading.Tasks;

namespace StravaSharp.Tests.Integration
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
            var client = await TestHelper.CreateStravaClient();
            var athlete = await client.Athletes.GetCurrent();
            Assert.That(athlete?.FirstName, Is.Not.Null);
            Assert.That(athlete.LastName, Is.Not.Null);
        }

        [Test]
        public async Task GetAthlete()
        {
            var client = await TestHelper.CreateStravaClient();
            var athlete = await client.Athletes.Get(TestAthleteId);
            Assert.That(athlete?.FirstName, Is.EqualTo("Gabor"));
            Assert.That(athlete.LastName, Is.EqualTo("Nemeth"));
        }
    }
}
