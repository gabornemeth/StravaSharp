using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            _restClient = new RestClient(StravaClient.ApiBaseUrl) { Authenticator = authenticator };
            Athletes = new AthleteClient(this);
            Activities = new ActivityClient(this);
            Segments = new SegmentClient(this);
            Clubs = new ClubClient(this);
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
        /// <summary>
        /// Clubs endpoint
        /// </summary>
        public ClubClient Clubs { get; private set; }
    }
}
