using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StravaSharp.FakeStrava
{
    internal class FakeActivityClient : IActivityClient
    {
        private FakeClient fakeClient;
        internal const long cActivityIdBase = 4000;

        Dictionary<long, Activity> activities;
        Dictionary<long, List<Activity>> activitiesByAthlete;

        public FakeActivityClient(FakeClient fakeClient)
        {
            this.fakeClient = fakeClient;
            // TODO - add activities
            // TODO - add segment efforts to activities
            // TOOD - add activities to athletes
            FakeClientConfig config = fakeClient.Config;
            activities = new Dictionary<long, Activity>(config.NumActivitiesPerAthlete * config.NumAthletes);
            activitiesByAthlete = new Dictionary<long, List<Activity>>(config.NumAthletes);
            foreach(int athlete in fakeClient.Athletes.Athletes.Keys)
            {
                activitiesByAthlete[athlete] = new List<Activity>();
            }
            long activityId = cActivityIdBase;
            for(int ath=0; ath<config.NumAthletes;++ath)
            {
                Athlete currAth = fakeClient.Athletes.Athletes[FakeAthleteClient.cAthleteIdBase + ath];
                long startSegment = (activityId % config.SegmentsOverall) + FakeSegmentClient.cSegmentIdBase;
                float athleteSpeed = config.MinAthleteSpeedKph + (config.MaxAthleteSpeedKph - config.MinAthleteSpeedKph) * ath / (float)config.NumAthletes;
                int elapsedTime = (int)(config.ActivityLengthMetres / (athleteSpeed * 3.6F)); // 3.6=kph to m/sec
                DateTime startDateLocal = config.MostRecentActivity.AddDays(-1F / config.ActivitiesPerDayPerAthlete);
                for (int athActivity=0; athActivity<config.NumActivitiesPerAthlete;++athActivity)
                {
                    Activity activity = new Activity
                    {
                        Id = activityId,
                        Name = $"Activity{activityId}",
                        Athlete = currAth,
                        Distance = config.ActivityLengthMetres,
                        AthleteCount=1,
                        ElapsedTime=elapsedTime,
                        AverageSpeed=athleteSpeed,
                        Calories=elapsedTime/5F,
                        StartDateLocal=startDateLocal,
                        
                    };
                    activities[activityId]=activity;
                    activitiesByAthlete[currAth.Id].Add(activity);
                    activity.SegmentEfforts = new List<SegmentEffort>(config.SegmentsPerActivity);
                    for(int segeff=0; segeff<config.SegmentsPerActivity; ++segeff)
                    {
                        SegmentEffort se = fakeClient.SegmentEfforts.Create();
                        se.Activity = activity;
                        se.Athlete = currAth;
                        se.Distance = config.SegmentLengthMetres;
                        se.ElapsedTime = elapsedTime / config.SegmentsPerActivity;
                        se.MovingTime = se.ElapsedTime;
                        se.Segment = fakeClient.Segments.Segments[startSegment + segeff];
                        se.Name = se.Segment.Name;
                        se.StartDateLocal = startDateLocal.AddSeconds((float)segeff / config.SegmentsPerActivity * elapsedTime);
                        se.StartDate = se.StartDateLocal;
                        se.AvgHeartRate = 99;
                        fakeClient.Segments.SegmentToEfforts[se.Segment.Id].Add(se);
                        activity.SegmentEfforts.Add(se);
                    }
                    ++activityId;
                }
            }

        }

        public Task Delete(Activity activity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Activity> Get(long activityId, bool includeAllEfforts = true)
        {
            return fakeClient.MakeTask(activities[activityId]);
        }

        public Task<IReadOnlyList<Stream>> GetActivityStreams(ActivityMeta activity, params StreamType[] types)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Stream>> GetActivityStreams(long activityId, params StreamType[] types)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Activity> CurrentAthleteActivities()
        {
            int athleteId = fakeClient.Athletes.CurrentAthleteId;
            return activitiesByAthlete[athleteId];
        }

        public Task<IReadOnlyList<ActivitySummary>> GetAthleteActivities(int page = 0, int itemsPerPage = 0)
        {
            Task<IReadOnlyList<ActivitySummary>> result = fakeClient.MakeTask((IReadOnlyList<ActivitySummary>)(CurrentAthleteActivities().ToList()));
            return itemsPerPage > 0 ? fakeClient.Paginate(result,page,itemsPerPage) : result;
        }

        public Task<IReadOnlyList<ActivitySummary>> GetAthleteActivities(DateTime before, DateTime after)
        {
            IReadOnlyList<ActivitySummary> result = CurrentAthleteActivities().Where(a => a.StartDate < before && a.StartDate > after).ToList();
            return fakeClient.MakeTask(result);
        }

        public Task<IReadOnlyList<ActivitySummary>> GetAthleteActivitiesAfter(DateTime after)
        {
            IReadOnlyList<ActivitySummary> result = CurrentAthleteActivities().Where(a => a.StartDate > after).ToList();
            return fakeClient.MakeTask(result);
        }

        public Task<IReadOnlyList<ActivitySummary>> GetAthleteActivitiesBefore(DateTime before)
        {
            IReadOnlyList<ActivitySummary> result = CurrentAthleteActivities().Where(a => a.StartDate < before).ToList();
            return fakeClient.MakeTask(result);

        }

        public Task<IReadOnlyList<Comment>> GetComments(long activityId, int page = 0, int itemsPerPage = 0)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<AthleteSummary>> GetKudoers(long activityId, int page = 0, int itemsPerPage = 0)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<ActivitySummary>> GetLaps(long activityId)
        {
            throw new NotImplementedException();
        }

        public Task<UploadStatus> GetUploadStatus(long id)
        {
            throw new NotImplementedException();
        }

        public Task<UploadStatus> Upload(ActivityType activityType, DataType dataType, System.IO.Stream input, string fileName, string name = null, string description = null, bool @private = false, bool commute = false, string externalId = null)
        {
            throw new NotImplementedException();
        }
    }
}