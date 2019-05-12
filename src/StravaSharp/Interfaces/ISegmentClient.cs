using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StravaSharp
{
    public interface ISegmentClient
    {
        Task<IReadOnlyList<ISegmentSummary>> Explore(LatLng southWest, LatLng northEast, ActivityType activityType = ActivityType.Ride);
        Task<ISegment> Get(long segmentId);
        Task<IEnumerable<ISegmentEffort>> GetEfforts(long segmentId);
        Task<IEnumerable<ISegmentEffort>> GetEfforts(long segmentId, DateTime startDateLocal, DateTime endDateLocal);
        Task<IEnumerable<ISegmentEffort>> GetEfforts(long segmentId, DateTime startDateLocal, DateTime endDateLocal, int page, int perPage);
        Task<IEnumerable<ISegmentEffort>> GetEfforts(long segmentId, int athleteId);
        Task<IEnumerable<ISegmentEffort>> GetEfforts(long segmentId, int athleteId, DateTime startDateLocal, DateTime endDateLocal);
        Task<IEnumerable<ISegmentEffort>> GetEfforts(long segmentId, int athleteId, DateTime startDateLocal, DateTime endDateLocal, int page, int perPage);
        Task<IEnumerable<ISegmentEffort>> GetEfforts(long segmentId, int page, int perPage);
        Task<IEnumerable<ISegmentEffort>> GetEfforts(long segmentId, int athleteId, int page, int perPage);
        Task<IReadOnlyList<IStream>> GetEffortStreams(ISegmentEffort effort, params StreamType[] types);
        Task<IReadOnlyList<IStream>> GetEffortStreams(long segmentEffortId, params StreamType[] types);
        Task<ILeaderboard> GetLeaderboard(long segmentId, Gender? gender, AgeGroup? ageGroup);
        Task<ILeaderboard> GetLeaderboard(long segmentId, int page, int perPage, Gender? gender, AgeGroup? ageGroup);
        Task<IReadOnlyList<IStream>> GetSegmentStreams(ISegmentSummary segment, params StreamType[] types);
        Task<IReadOnlyList<IStream>> GetSegmentStreams(long segmentId, params StreamType[] types);
    }
}