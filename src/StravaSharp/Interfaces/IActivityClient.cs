using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace StravaSharp
{
    public interface IActivityClient
    {
        Task Delete(Activity activity);
        Task Delete(long id);
        Task<Activity> Get(long activityId, bool includeAllEfforts = true);
        Task<IReadOnlyList<Stream>> GetActivityStreams(ActivityMeta activity, params StreamType[] types);
        Task<IReadOnlyList<Stream>> GetActivityStreams(long activityId, params StreamType[] types);
        Task<IReadOnlyList<ActivitySummary>> GetAthleteActivities(int page = 0, int itemsPerPage = 0);
        Task<IReadOnlyList<ActivitySummary>> GetAthleteActivities(DateTime before, DateTime after);
        Task<IReadOnlyList<ActivitySummary>> GetAthleteActivitiesAfter(DateTime after);
        Task<IReadOnlyList<ActivitySummary>> GetAthleteActivitiesBefore(DateTime before);
        Task<IReadOnlyList<Comment>> GetComments(long activityId, int page = 0, int itemsPerPage = 0);
        Task<IReadOnlyList<AthleteSummary>> GetKudoers(long activityId, int page = 0, int itemsPerPage = 0);
        Task<IReadOnlyList<ActivitySummary>> GetLaps(long activityId);
        Task<UploadStatus> GetUploadStatus(long id);
        Task<UploadStatus> Upload(ActivityType activityType, DataType dataType, System.IO.Stream input, string fileName, string name = null, string description = null, bool @private = false, bool commute = false, string externalId = null);
    }
}