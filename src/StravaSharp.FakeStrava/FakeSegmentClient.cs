using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StravaSharp.FakeStrava
{
    internal class FakeSegmentClient : ISegmentClient
    {
        private FakeClient fakeClient;
        internal const long cSegmentIdBase = 3000;
        internal const long cSegmentEffortIdBase = 5000;
        internal readonly Dictionary<long, Segment> Segments;
        internal readonly Dictionary<long, List<SegmentEffort>> SegmentToEfforts;


        public FakeSegmentClient(FakeClient fakeClient)
        {
            this.fakeClient = fakeClient;
            Segments = new Dictionary<long, Segment>(fakeClient.Config.SegmentsOverall);
            SegmentToEfforts = new Dictionary<long, List<SegmentEffort>>();
            CreateSegments();
        }

        private void CreateSegments()
        {
            FakeClientConfig config = fakeClient.Config;
            float segmentCircleCircumference = config.SegmentLengthMetres * config.SegmentsOverall;
            float segmentRadius = (float)(segmentCircleCircumference / 2 / Math.PI);
            float segmentArc = (float)(2 * Math.PI / config.SegmentsOverall);
            for(int i=0; i<config.SegmentsOverall;++i)
            {
                long segmentId = cSegmentIdBase + i;
                Segments[segmentId] = new Segment
                {
                    Id = segmentId,
                    City = config.SegmentCity,
                    Country = config.SegmentCountry,
                    ActivityType = ActivityType.Ride,
                    Distance = config.SegmentLengthMetres,
                    Name = $"Segment{segmentId}",
                    StartLatLng = config.SegmentCentre.MoveMetresRadians(segmentRadius, i * segmentArc),
                    EndLatLng = config.SegmentCentre.MoveMetresRadians(segmentRadius, (i + 1) * segmentArc)
                };
                SegmentToEfforts[segmentId] = new List<SegmentEffort>();
            }
        }

        internal void AddEffortToSegment(long segmentId, SegmentEffort effort)
        {
            SegmentToEfforts[segmentId].Add(effort);
        }

        public Task<IReadOnlyList<SegmentSummary>> Explore(LatLng southWest, LatLng northEast, ActivityType activityType = ActivityType.Ride)
        {
            List<SegmentSummary> segs = new List<SegmentSummary>();
            return fakeClient.MakeTask<IReadOnlyList<SegmentSummary>>(segs);
        }

        public Task<Segment> Get(long segmentId)
        {
            return fakeClient.MakeTask(Segments[segmentId]);
        }



        public Task<IReadOnlyList<SegmentEffort>> GetEfforts(long segmentId)
        {
            IReadOnlyList<SegmentEffort> efforts = SegmentToEfforts[segmentId];
            return fakeClient.MakeTask(efforts);
        }

        public Task<IReadOnlyList<SegmentEffort>> GetEfforts(long segmentId, DateTime startDateLocal, DateTime endDateLocal)
        {
            IReadOnlyList<SegmentEffort> efforts = SegmentToEfforts[segmentId].Where(se => se.StartDateLocal >= startDateLocal && se.StartDateLocal <= endDateLocal).ToList();
            return fakeClient.MakeTask(efforts);
        }

        public Task<IReadOnlyList<SegmentEffort>> GetEfforts(long segmentId, DateTime startDateLocal, DateTime endDateLocal, int page, int perPage)
        {
            return fakeClient.Paginate(GetEfforts(segmentId, startDateLocal, endDateLocal), page, perPage);
        }

        public Task<IReadOnlyList<SegmentEffort>> GetEfforts(long segmentId, int athleteId)
        {
            IReadOnlyList<SegmentEffort> efforts = SegmentToEfforts[segmentId].Where(se => se.Athlete.Id==athleteId).ToList();
            return fakeClient.MakeTask(efforts);
        }

        public Task<IReadOnlyList<SegmentEffort>> GetEfforts(long segmentId, int athleteId, DateTime startDateLocal, DateTime endDateLocal)
        {
            IReadOnlyList<SegmentEffort> efforts = SegmentToEfforts[segmentId].Where(se => se.Athlete.Id==athleteId && se.StartDateLocal >= startDateLocal && se.StartDateLocal <= endDateLocal).ToList();
            return fakeClient.MakeTask(efforts);
        }

        public Task<IReadOnlyList<SegmentEffort>> GetEfforts(long segmentId, int athleteId, DateTime startDateLocal, DateTime endDateLocal, int page, int perPage)
        {
            return fakeClient.Paginate(GetEfforts(segmentId, athleteId, startDateLocal, endDateLocal), page, perPage);
        }

        public Task<IReadOnlyList<SegmentEffort>> GetEfforts(long segmentId, int page, int perPage)
        {
            return fakeClient.Paginate(GetEfforts(segmentId), page, perPage);
        }

        public Task<IReadOnlyList<SegmentEffort>> GetEfforts(long segmentId, int athleteId, int page, int perPage)
        {
            return fakeClient.Paginate(GetEfforts(segmentId, athleteId), page, perPage);
        }

        public Task<IReadOnlyList<Stream>> GetEffortStreams(SegmentEffort effort, params StreamType[] types)
        {
            throw new NotImplementedException("GetEffortStreams");
        }

        public Task<IReadOnlyList<Stream>> GetEffortStreams(long segmentEffortId, params StreamType[] types)
        {
            throw new NotImplementedException("GetEffortStreams");
        }

        public Task<Leaderboard> GetLeaderboard(long segmentId, Gender? gender, AgeGroup? ageGroup)
        {
            throw new NotImplementedException("GetLeaderboard");
        }

        public Task<Leaderboard> GetLeaderboard(long segmentId, int page, int perPage, Gender? gender, AgeGroup? ageGroup)
        {
            throw new NotImplementedException("GetLeaderboard");
        }

        public Task<IReadOnlyList<Stream>> GetSegmentStreams(SegmentSummary segment, params StreamType[] types)
        {
            throw new NotImplementedException("GetEffortStreams");
        }

        public Task<IReadOnlyList<Stream>> GetSegmentStreams(long segmentId, params StreamType[] types)
        {
            throw new NotImplementedException("GetEffortStreams");
        }
    }
}