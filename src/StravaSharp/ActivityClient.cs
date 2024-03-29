﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

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
            var request = new RestRequest(EndPoint + "/{id}", Method.Get);
            request.AddParameter("id", activityId, ParameterType.UrlSegment);
            return await _client.RestClient.ExecuteForJson<Activity>(request);
        }

        public Task<IEnumerable<ActivitySummary>> GetAthleteActivities(int page = 0, int itemsPerPage = 0)
        {
            return GetAthleteActivities(DateTime.MinValue, DateTime.MinValue, page, itemsPerPage);
        }

        public Task<IEnumerable<ActivitySummary>> GetAthleteActivities(DateTime before, DateTime after)
        {
            return GetAthleteActivities(before, after, 0, 0);
        }

        public Task<IEnumerable<ActivitySummary>> GetAthleteActivitiesBefore(DateTime before)
        {
            return GetAthleteActivities(before, DateTime.MinValue, 0, 0);
        }

        public Task<IEnumerable<ActivitySummary>> GetAthleteActivitiesAfter(DateTime after)
        {
            return GetAthleteActivities(DateTime.MinValue, after, 0, 0);
        }

        private async Task<IEnumerable<ActivitySummary>> GetAthleteActivities(DateTime before, DateTime after, int page, int itemsPerPage)
        {
            var request = new RestRequest("/api/v3/athlete/activities");
            if (before != DateTime.MinValue)
                request.AddQueryParameter("before", before.GetSecondsSinceUnixEpoch());
            if (after != DateTime.MinValue)
                request.AddQueryParameter("after", after.GetSecondsSinceUnixEpoch());
            request.AddPaging(page, itemsPerPage);
            return await _client.RestClient.ExecuteForJson<ActivitySummary[]>(request);
        }

        public async Task<UploadStatus> Upload(ActivityType activityType, DataType dataType, System.IO.Stream input, string fileName, string name = null, string description = null,
            bool @private = false, bool commute = false, string externalId = null)
        {
            //var httpClient = new HttpClient();
            //httpClient.BaseAddress = _client.RestClient.BaseUrl;

            //var msg = new HttpRequestMessage(HttpMethod.Post, "api/v3/uploads");
            //var content = new MultipartFormDataContent();
            //content.Add(new StreamContent(input, 512), "file", Uri.EscapeDataString(fileName));
            //var c = new StringContent("fit");
            //c.Headers.Clear();
            //content.Add(new StringContent(EnumHelper.ToString(dataType), "\"data_type\"");
            //content.Add(new StringContent(EnumHelper.ToString(activityType)), "activity_type");
            //content.Add(new StringContent(@private ? "1" : "0"), "private");
            //content.Add(new StringContent(commute ? "1" : "0"), "commute");
            //msg.Content = content;

            //_client.Authenticator.Authenticate(msg);
            //var requestAsString = await msg.Content.ReadAsStringAsync();
            //return await httpClient.SendAsync<UploadStatus>(msg);

            var request = new RestRequest("/api/v3/uploads", Method.Post);
            //request.ContentCollectionMode = ContentCollectionMode.MultiPart;
            request.AddFile("file", () => input, Uri.EscapeDataString(fileName));

            // workaround: multipart form-data parameters has to be enclosed in ""
            // https://github.com/dotnet/corefx/issues/26886

            if (name != null)
            {
                request.AddParameter("\"name\"", name);
            }
            if (description != null)
            {
                request.AddParameter("\"description\"", description);
            }

            request.AddParameter("\"data_type\"", EnumHelper.ToString(dataType));
            request.AddParameter("\"activity_type\"", EnumHelper.ToString(activityType));
            request.AddParameter("\"private\"", @private ? 1 : 0);
            request.AddParameter("\"commute\"", commute ? 1 : 0);
            if (!string.IsNullOrWhiteSpace(externalId))
            {
                request.AddParameter("\"external_id\"", externalId);
            }
            return await _client.RestClient.ExecuteForJson<UploadStatus>(request);
        }

        public async Task<UploadStatus> GetUploadStatus(long id)
        {
            var request = new RestRequest("/api/v3/uploads/{id}", Method.Get);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            return await _client.RestClient.ExecuteForJson<UploadStatus>(request);
        }

        public async Task<Activity> Update(long id)
        {
            var request = new RestRequest(EndPoint + "/{id}", Method.Put);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            return await _client.RestClient.ExecuteForJson<Activity>(request);
        }

        /// <summary>
        /// List the laps of an activity.
        /// </summary>
        /// <param name="activityId">Identifier of the activity.</param>
        /// <returns>List of laps.</returns>
        public async Task<IEnumerable<ActivitySummary>> GetLaps(long activityId)
        {
            var request = new RestRequest("/api/v3/activities/{id}/laps", Method.Get);
            request.AddParameter("id", activityId, ParameterType.UrlSegment);
            return await _client.RestClient.ExecuteForJson<ActivitySummary[]>(request);
        }

        public async Task<IEnumerable<Comment>> GetComments(long activityId, int page = 0, int itemsPerPage = 0)
        {
            var request = new RestRequest("/api/v3/activities/{id}/comments", Method.Get);
            request.AddParameter("id", activityId, ParameterType.UrlSegment);
            if (page != 0)
                request.AddParameter("page", page);
            if (itemsPerPage != 0)
                request.AddParameter("per_page", itemsPerPage);
            return await _client.RestClient.ExecuteForJson<Comment[]>(request);
        }

        /// <summary>
        /// List activity kudoers
        /// </summary>
        /// <param name="activityId">Identifier of the activity.</param>
        /// <param name="page"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AthleteSummary>> GetKudoers(long activityId, int page = 0, int itemsPerPage = 0)
        {
            var request = new RestRequest("/api/v3/activities/{id}/kudos", Method.Get);
            request.AddParameter("id", activityId, ParameterType.UrlSegment);
            if (page != 0)
                request.AddParameter("page", page);
            if (itemsPerPage != 0)
                request.AddParameter("per_page", itemsPerPage);
            return await _client.RestClient.ExecuteForJson<AthleteSummary[]>(request);
        }

        public Task<IEnumerable<Stream>> GetActivityStreams(ActivityMeta activity, params StreamType[] types)
        {
            return GetActivityStreams(activity.Id, types);
        }

        public async Task<IEnumerable<Stream>> GetActivityStreams(long activityId, params StreamType[] types)
        {
            var request = new RestRequest("/api/v3/activities/{id}/streams/{types}", Method.Get);
            request.AddParameter("id", activityId, ParameterType.UrlSegment);
            request.AddParameter("types", EnumHelper.ToString<StreamType>(types), ParameterType.UrlSegment);
            return await _client.RestClient.ExecuteForJson<Stream[]>(request);
        }

        /// <summary>
        /// Summit Feature. Returns the zones of a given activity. Requires activity:read for Everyone and Followers activities. Requires activity:read_all for Only Me activities.
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ActivityZone>> GetActivityZones(long activityId)
        {
            var request = new RestRequest("/api/v3/activities/{id}/zones", Method.Get);
            request.AddParameter("id", activityId, ParameterType.UrlSegment);

            return await _client.RestClient.ExecuteForJson<ActivityZone[]>(request);
        }
    }
}
