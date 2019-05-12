using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace StravaSharp
{
    public interface IActivityClient
    {
        Task Delete(IActivity activity);
        Task Delete(long id);
        Task<IActivity> Get(long activityId, bool includeAllEfforts = true);
        Task<IReadOnlyList<IStream>> GetActivityStreams(IActivityMeta activity, params StreamType[] types);
        Task<IReadOnlyList<IStream>> GetActivityStreams(long activityId, params StreamType[] types);
        Task<IReadOnlyList<IActivitySummary>> GetAthleteActivities(int page = 0, int itemsPerPage = 0);
        Task<IReadOnlyList<IActivitySummary>> GetAthleteActivities(DateTime before, DateTime after);
        Task<IReadOnlyList<IActivitySummary>> GetAthleteActivitiesAfter(DateTime after);
        Task<IReadOnlyList<IActivitySummary>> GetAthleteActivitiesBefore(DateTime before);
        Task<IReadOnlyList<IComment>> GetComments(long activityId, int page = 0, int itemsPerPage = 0);
        Task<IReadOnlyList<IAthleteSummary>> GetKudoers(long activityId, int page = 0, int itemsPerPage = 0);
        Task<IReadOnlyList<IActivitySummary>> GetLaps(long activityId);
        Task<UploadStatus> GetUploadStatus(long id);
        Task<UploadStatus> Upload(ActivityType activityType, DataType dataType, System.IO.Stream input, string fileName, string name = null, string description = null, bool @private = false, bool commute = false, string externalId = null);
    }
}