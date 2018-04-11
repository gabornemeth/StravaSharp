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
    public class Client
    {
        private RestClient _restClient;

        internal RestClient RestClient => _restClient;

        public IAuthenticator Authenticator { get; private set; }

        public Client(IAuthenticator authenticator)
        {
            Authenticator = authenticator;
            _restClient = new RestClient(StravaClient.ApiBaseUrl)
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
