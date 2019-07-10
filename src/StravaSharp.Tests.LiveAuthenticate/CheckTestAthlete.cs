using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{

    [TestFixture]
    public class CheckTestAthlete : BaseLiveTestClass
    {

        private const long cAthleteId = 42170685;

        [Test]
        public async Task CheckTestAthleteData()
        {
            Athlete current =await StravaTestAthleteClient.Athletes.GetCurrent();
            Assert.AreEqual("Test", current.FirstName);
            Assert.AreEqual("Athlete", current.LastName);
            Assert.AreEqual(cAthleteId, current.Id);
            Assert.AreEqual("Cambridge", current.City);
        }

        [Test]
        public async Task CheckActivities()
        {
            IActivityClient activityClient = StravaTestAthleteClient.Activities;
            IReadOnlyList<ActivitySummary> activities = await activityClient.GetAthleteActivities();
            Assert.AreEqual(2, activities.Count);
            ActivitySummary act = activities[1];
            Assert.AreEqual(cAthleteId, act.Athlete.Id);
            Assert.AreEqual(true, act.Manual);
            Assert.AreEqual(2366117371, act.Id);
            Assert.AreEqual(3723, act.ElapsedTime);
            act = activities[0];
            Assert.AreEqual(cAthleteId, act.Athlete.Id);
            Assert.AreEqual(false, act.Manual);
            Assert.AreEqual("Cloned test ride", act.Name);
            Assert.AreEqual(2437245009, act.Id);
            Assert.AreEqual(7594, act.ElapsedTime);
            var detail = await activityClient.Get(act.Id);
            Assert.AreEqual("Cloned test ride", detail.Name);
            // Note that we can't guarantee the segmenteffort count will stay static as people
            // may create new segments, which will then appear as efforts for this ride
            Assert.GreaterOrEqual(24, detail.SegmentEfforts.Count);
            var noDetail = await activityClient.Get(act.Id, false);
            Assert.AreEqual("Cloned test ride", noDetail.Name);
            // The docs suggest this should be 0, but in reality we do always get the 
            // segment details.  So we can detect this, test it.
            Assert.GreaterOrEqual(24, noDetail.SegmentEfforts.Count);

        }
    }
}
