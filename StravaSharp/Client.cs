using RestSharp.Portable;
using RestSharp.Portable.Authenticators.OAuth2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StravaSharp
{
    /// <summary>
    /// Strava client
    /// </summary>
    public class Client
    {
        private IAuthenticator _authenticator;
        private RestClient _restClient;

        public Client(StravaClient client, IAuthenticator authenticator)
        {
            _authenticator = authenticator;
            _restClient = new RestClient(StravaClient.ApiBaseUrl);

            _restClient.Authenticator = authenticator.RestSharpAuthenticator;
        }

        public async Task<List<Activity>> GetActivities()
        {
            var request = new RestRequest("/api/v3/athlete/activities");
            var response = await _restClient.Execute<List<Activity>>(request);

            return response.Data;
        }


        public Task DeleteActivity(Activity activity)
        {
            return DeleteActivity(activity.Id);
        }

        public async Task DeleteActivity(string id)
        {
            var request = new RestRequest("/api/v3/activities/{id}", HttpMethod.Delete);
            request.AddParameter("id", id);
            await _restClient.Execute(request);
        }

        public async Task<UploadResult> UploadActivity(ActivityType activityType, DataType dataType, Stream input, string fileName)
        {
            var request = new RestRequest("/api/v3/uploads", HttpMethod.Post);
            request.AddParameter("activity_type", activityType.ToStravaType());
            request.AddParameter("data_type", dataType.ToStravaType());
            request.AddFile("file", input, fileName);
            var response = await _restClient.Execute<UploadResult>(request);
            return response.Data;
        }
    }
}
