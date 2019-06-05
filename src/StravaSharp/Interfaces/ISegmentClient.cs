using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StravaSharp
{
    public interface ISegmentClient
    {
        Task<IReadOnlyList<SegmentSummary>> Explore(LatLng southWest, LatLng northEast, ActivityType activityType = ActivityType.Ride);
        Task<Segment> Get(long segmentId);
        Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId);
        Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId, DateTime startDateLocal, DateTime endDateLocal);
        Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId, DateTime startDateLocal, DateTime endDateLocal, int page, int perPage);
        Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId, int athleteId);
        Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId, int athleteId, DateTime startDateLocal, DateTime endDateLocal);
        Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId, int athleteId, DateTime startDateLocal, DateTime endDateLocal, int page, int perPage);
        Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId, int page, int perPage);
        Task<IEnumerable<SegmentEffort>> GetEfforts(long segmentId, int athleteId, int page, int perPage);
        Task<IReadOnlyList<Stream>> GetEffortStreams(SegmentEffort effort, params StreamType[] types);
        Task<IReadOnlyList<Stream>> GetEffortStreams(long segmentEffortId, params StreamType[] types);
        Task<Leaderboard> GetLeaderboard(long segmentId, Gender? gender, AgeGroup? ageGroup);
        Task<Leaderboard> GetLeaderboard(long segmentId, int page, int perPage, Gender? gender, AgeGroup? ageGroup);
        Task<IReadOnlyList<Stream>> GetSegmentStreams(SegmentSummary segment, params StreamType[] types);
        Task<IReadOnlyList<Stream>> GetSegmentStreams(long segmentId, params StreamType[] types);
    }
}