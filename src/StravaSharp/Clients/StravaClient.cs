using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;

namespace StravaSharp
{
    class MyHttpClientFactory : RestSharp.Portable.HttpClient.Impl.DefaultHttpClientFactory
    {
        public override IHttpRequestMessage CreateRequestMessage(IRestClient client, IRestRequest request, IList<Parameter> parameters)
        {
            var msg = base.CreateRequestMessage(client, request, parameters);
            return msg;
        }

        protected override HttpMessageHandler CreateMessageHandler(IRestClient client)
        {
            return new MyHttpMessageHandler();
        }
    }

    class MyHttpMessageHandler : HttpClientHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            var responseAsString = await response.Content.ReadAsStringAsync();
            return response;
        }
    }

    /// <summary>
    /// Strava client
    /// </summary>
    internal class StravaClient : IStravaClient
    {
        internal RestClient RestClient { get; }

        public IAuthenticator Authenticator { get; private set; }

        public StravaClient(IAuthenticator authenticator)
        {
            Authenticator = authenticator;
            RestClient = new RestClient(StravaOAuth2Client.ApiBaseUrl)
            {
                Authenticator = authenticator,
#if DEBUG
                HttpClientFactory = new MyHttpClientFactory(),
#endif
            };
            Athletes = new AthleteClient(this);
            Activities = new ActivityClient(this);
            Segments = new SegmentClient(this);
            Clubs = new ClubClient(this);
        }

        public IAthleteClient Athletes { get; private set; }
        /// <summary>
        /// Activities endpoint
        /// </summary>
        public IActivityClient Activities { get; private set; }
        /// <summary>
        /// Segments endpoint
        /// </summary>
        public ISegmentClient Segments { get; private set; }
        /// <summary>
        /// Clubs endpoint
        /// </summary>
        public IClubClient Clubs { get; private set; }
    }
}
