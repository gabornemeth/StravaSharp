using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestSharp.Portable;

namespace StravaSharp
{
    public class ActivityClient
    {
        private Client _client;
        private const string EndPoint = "/api/v3/activities";

        internal ActivityClient(Client client)
        {
            _client = client;
        }

        public async Task<Activity> Get(int activityId, bool includeAllEfforts = true)
        {
            var request = new RestRequest(EndPoint + "/{id}", HttpMethod.Get);
            request.AddParameter("id", activityId, ParameterType.UrlSegment);
            var response = await _client.RestClient.Execute<Activity>(request);
            return response.Data;
        }

        public Task<List<ActivitySummary>> GetAthleteActivities(int page = 0, int itemsPerPage = 0)
        {
            return GetAthleteActivities(DateTime.MinValue, DateTime.MinValue, page, itemsPerPage);
        }

        public Task<List<ActivitySummary>> GetAthleteActivities(DateTime before, DateTime after)
        {
            return GetAthleteActivities(before, after, 0, 0);
        }

        public Task<List<ActivitySummary>> GetAthleteActivitiesBefore(DateTime before)
        {
            return GetAthleteActivities(before, DateTime.MinValue, 0, 0);
        }

        public Task<List<ActivitySummary>> GetAthleteActivitiesAfter(DateTime after)
        {
            return GetAthleteActivities(DateTime.MinValue, after, 0, 0);
        }

        private async Task<List<ActivitySummary>> GetAthleteActivities(DateTime before, DateTime after, int page, int itemsPerPage)
        {
            var request = new RestRequest("/api/v3/athlete/activities");
            if (before != DateTime.MinValue)
                request.AddParameter("before", before.GetSecondsSinceUnixEpoch());
            if (after != DateTime.MinValue)
                request.AddParameter("after", after.GetSecondsSinceUnixEpoch());
            if (page != 0)
                request.AddParameter("page", page);
            if (itemsPerPage != 0)
                request.AddParameter("per_page", itemsPerPage);
            var response = await _client.RestClient.Execute<List<ActivitySummary>>(request);

            return response.Data;
        }

        public async Task<UploadStatus> Upload(ActivityType activityType, DataType dataType, System.IO.Stream input, string fileName)
        {
            var request = new RestRequest("/api/v3/uploads?data_type={data_type}&activity_type={activity_type}", HttpMethod.Post);
            request.ContentCollectionMode = ContentCollectionMode.MultiPart;
            request.AddParameter("data_type", "fit", ParameterType.UrlSegment);
            request.AddParameter("activity_type", EnumHelper.ToString(activityType), ParameterType.UrlSegment);
            request.AddFile("file", input, Uri.EscapeDataString(fileName));
            var response = await _client.RestClient.Execute<UploadStatus>(request);
            return response.Data;
        }

        public async Task<UploadStatus> GetUploadStatus(int id)
        {
            var request = new RestRequest("/api/v3/uploads/{id}", HttpMethod.Get);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            var response = await _client.RestClient.Execute<UploadStatus>(request);
            return response.Data;
        }

        public Task Delete(Activity activity)
        {
            return Delete(activity.Id);
        }

        public async Task Delete(int id)
        {
            var request = new RestRequest(EndPoint + "/{id}", HttpMethod.Delete);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            await _client.RestClient.Execute(request);
        }

    }
}
