using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using RestSharp.Portable;
using System.Text;
using Newtonsoft.Json;

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

        public async Task<Activity> Get(long activityId, bool includeAllEfforts = true)
        {
            var request = new RestRequest(EndPoint + "/{id}", Method.GET);
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
                request.AddQueryParameter("before", before.GetSecondsSinceUnixEpoch());
            if (after != DateTime.MinValue)
                request.AddQueryParameter("after", after.GetSecondsSinceUnixEpoch());
            request.AddPaging(page, itemsPerPage);
            var response = await _client.RestClient.Execute<List<ActivitySummary>>(request);

            return response.Data;
        }

        public async Task<UploadStatus> Upload(ActivityType activityType, DataType dataType, System.IO.Stream input, string fileName, string name = null, string description = null,
            bool @private = false, bool commute = false)
        {
            var request = new RestRequest("/api/v3/uploads?data_type={data_type}&activity_type={activity_type}&private={private}&commute={commute}", Method.POST);
            if (name != null)
            {
                request.Resource += "&name={name}";
                request.AddParameter("name", name, ParameterType.UrlSegment);
            }
            if (description != null)
            {
                request.Resource += "&description={description}";
                request.AddParameter("description", description, ParameterType.UrlSegment);
            }

            request.ContentCollectionMode = ContentCollectionMode.MultiPart;
            request.AddParameter("data_type", dataType, ParameterType.UrlSegment);
            request.AddParameter("activity_type", EnumHelper.ToString(activityType), ParameterType.UrlSegment);
            request.AddParameter("private", @private ? 1 : 0, ParameterType.UrlSegment);
            request.AddParameter("commute", commute ? 1 : 0, ParameterType.UrlSegment);
            request.AddFile("file", input, Uri.EscapeDataString(fileName));
            var response = await _client.RestClient.Execute<UploadStatus>(request);
            return response.Data;
        }

        public async Task<UploadStatus> GetUploadStatus(int id)
        {
            var request = new RestRequest("/api/v3/uploads/{id}", Method.GET);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            var response = await _client.RestClient.Execute<UploadStatus>(request);
            return response.Data;
        }

        /// <summary>
        /// Delete an activity.
        /// </summary>
        /// <param name="activity">Activity to delete.</param>
        /// <returns></returns>
        public Task Delete(Activity activity)
        {
            return Delete(activity.Id);
        }

        /// <summary>
        /// Delete an activity.
        /// </summary>
        /// <param name="id">Identifier of the activity.</param>
        /// <returns></returns>
        public async Task Delete(long id)
        {
            var request = new RestRequest(EndPoint + "/{id}", Method.DELETE);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            await _client.RestClient.Execute(request);
        }

        /// <summary>
        /// List the recent activities performed by the current athlete and those they are following.
        /// </summary>
        /// <param name="page">Page number (optional).</param>
        /// <param name="itemsPerPage">Number of items per page (optional).</param>
        /// <returns>List of activities.</returns>
        public Task<List<ActivitySummary>> GetFollowingActivities(int page = 0, int itemsPerPage = 0)
        {
            return GetFollowingActivities(DateTime.MinValue, page, itemsPerPage);
        }

        /// <summary>
        /// List the recent activities performed by the current athlete and those they are following.
        /// </summary>
        /// <param name="before">Result will start with activities whose start_date is before this value.</param>
        /// <returns>List of activities.</returns>
        public Task<List<ActivitySummary>> GetFollowingActivities(DateTime before)
        {
            return GetFollowingActivities(before, 0, 0);
        }

        /// <summary>
        /// List the recent activities performed by the current athlete and those they are following.
        /// </summary>
        /// <param name="before">Result will start with activities whose start_date is before this value.</param>
        /// <param name="page">Page number (optional).</param>
        /// <param name="itemsPerPage">Number of items per page (optional).</param>
        /// <returns>List of activities.</returns>
        private async Task<List<ActivitySummary>> GetFollowingActivities(DateTime before, int page, int itemsPerPage)
        {
            var request = new RestRequest("/api/v3/activities/following");
            if (before != DateTime.MinValue)
                request.AddQueryParameter("before", before.GetSecondsSinceUnixEpoch());
            if (page != 0)
                request.AddParameter("page", page);
            if (itemsPerPage != 0)
                request.AddParameter("per_page", itemsPerPage);
            var response = await _client.RestClient.Execute<List<ActivitySummary>>(request);

            return response.Data;
        }

        /// <summary>
        /// List the laps of an activity.
        /// </summary>
        /// <param name="activityId">Identifier of the activity.</param>
        /// <returns>List of laps.</returns>
        public async Task<List<ActivitySummary>> GetLaps(long activityId)
        {
            var request = new RestRequest("/api/v3/activities/{id}/laps", Method.GET);
            request.AddParameter("id", activityId, ParameterType.UrlSegment);
            var response = await _client.RestClient.Execute<List<ActivitySummary>>(request);
            return response.Data;
        }

        /// <summary>
        /// Returns the activities that were matched as “with this group”. The number equals activity.athlete_count-1. Pagination is supported.
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        public async Task<List<ActivitySummary>> GetRelatedActivities(long activityId, int page = 0, int itemsPerPage = 0)
        {
            var request = new RestRequest("/api/v3/activities/{id}/related", Method.GET);
            request.AddParameter("id", activityId, ParameterType.UrlSegment);
            if (page != 0)
                request.AddParameter("page", page);
            if (itemsPerPage != 0)
                request.AddParameter("per_page", itemsPerPage);
            var response = await _client.RestClient.Execute<List<ActivitySummary>>(request);

            return response.Data;
        }


        public async Task<List<Comment>> GetComments(long activityId, int page = 0, int itemsPerPage = 0)
        {
            var request = new RestRequest("/api/v3/activities/{id}/comments", Method.GET);
            request.AddParameter("id", activityId, ParameterType.UrlSegment);
            if (page != 0)
                request.AddParameter("page", page);
            if (itemsPerPage != 0)
                request.AddParameter("per_page", itemsPerPage);
            var response = await _client.RestClient.Execute<List<Comment>>(request);

            return response.Data;
        }

        /// <summary>
        /// List activity kudoers
        /// </summary>
        /// <param name="activityId">Identifier of the activity.</param>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        public async Task<List<AthleteSummary>> GetKudoers(long activityId, int page = 0, int itemsPerPage = 0)
        {
            var request = new RestRequest("/api/v3/activities/{id}/kudos", Method.GET);
            request.AddParameter("id", activityId, ParameterType.UrlSegment);
            if (page != 0)
                request.AddParameter("page", page);
            if (itemsPerPage != 0)
                request.AddParameter("per_page", itemsPerPage);
            var response = await _client.RestClient.Execute<List<AthleteSummary>>(request);

            return response.Data;
        }

        public Task<List<Stream>> GetActivityStreams(ActivityMeta activity, params StreamType[] types)
        {
            return GetActivityStreams(activity.Id, types);
        }

        public async Task<List<Stream>> GetActivityStreams(long activityId, params StreamType[] types)
        {
            var request = new RestRequest("/api/v3/activities/{id}/streams/{types}", Method.GET);
            request.AddParameter("id", activityId, ParameterType.UrlSegment);
            request.AddParameter("types", EnumHelper.ToString<StreamType>(types), ParameterType.UrlSegment);
            var response = await _client.RestClient.Execute<List<Stream>>(request);
            return response.Data;
        }
    }
}
