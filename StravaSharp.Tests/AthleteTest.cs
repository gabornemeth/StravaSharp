using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    [TestFixture]
    public class AthleteTest
    {
        [Test]
        public async Task GetAthlete()
        {
            var client = TestHelper.CreateStravaClient();
            var athlete = await client.Athletes.GetCurrent();
            Assert.NotNull(athlete);
            Assert.AreEqual("gabor.nemeth.dev@gmail.com", athlete.Email);
            Assert.AreEqual("Extrava", athlete.FirstName);
        }
    }
}
