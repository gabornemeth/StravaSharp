using NUnit.Framework;
using StravaSharp;
using StravaSharp.FakeStrava;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakeTests
{
    public class FakeTests
    {
        IStravaClient client;
        FakeClientConfig config;
        [SetUp]
        public void Setup()
        {
            config = new FakeClientConfig();
            client = new FakeClient(config,null);
        }

        [Test]
        public async Task Test1()
        {
            IReadOnlyList<ActivitySummary> acts =await client.Activities.GetAthleteActivities();
            Assert.AreEqual(config.NumActivitiesPerAthlete, acts.Count);
            int numacts = acts.Count;
            for(int i=0; i<numacts;++i)
            {
                long actId = acts[i].Id;
                var activity = await client.Activities.Get(actId);
                Assert.AreEqual(config.SegmentsPerActivity, activity.SegmentEfforts.Count);
            }
        }
    }
}