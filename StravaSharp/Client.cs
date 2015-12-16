using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp.Portable.Authenticators.OAuth2.Infrastructure;
using RestSharp.Portable.HttpClient;

namespace StravaSharp
{
    /// <summary>
    /// Strava client
    /// </summary>
    public class Client
    {
        private RestClient _restClient;

        internal RestClient RestClient
        {
            get
            {
                return _restClient;
            }
        }

        public Client(IAuthenticator authenticator)
        {
            _restClient = new RestClient(StravaClient.ApiBaseUrl) { Authenticator = authenticator.RestSharpAuthenticator };
            Athletes = new AthleteClient(this);
            Activities = new ActivityClient(this);
            Segments = new SegmentClient(this);
        }

        public AthleteClient Athletes { get; private set; }
        /// <summary>
        /// Activities endpoint
        /// </summary>
        public ActivityClient Activities { get; private set; }
        /// <summary>
        /// Segments endpoint
        /// </summary>
        public SegmentClient Segments { get; private set; }

        public async Task<List<Stream>> GetActivityStreams(int activityId, params StreamType[] types)
        {
            var request = new RestRequest("/api/v3/activities/{id}/streams/{types}", Method.GET);
            request.AddParameter("id", activityId, ParameterType.UrlSegment);
            request.AddParameter("types", EnumHelper.ToString<StreamType>(types), ParameterType.UrlSegment);
            var response = await _restClient.Execute<List<Stream>>(request);
            return response.Data;
        }
    }
}
