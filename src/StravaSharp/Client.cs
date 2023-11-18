﻿using System.Collections.Generic;
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
        private readonly RestClient _restClient;

        virtual internal protected IRestClient RestClient => _restClient;

        public IAuthenticator Authenticator { get; }

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
            SegmentEfforts = new SegmentEffortsClient(this);
            Clubs = new ClubClient(this);
        }

        public AthleteClient Athletes { get; }
        /// <summary>
        /// Activities endpoint
        /// </summary>
        public ActivityClient Activities { get; }
        /// <summary>
        /// Segments endpoint
        /// </summary>
        public SegmentClient Segments { get; }

        /// <summary>
        /// Segment efforts endpoint
        /// </summary>
        public SegmentEffortsClient SegmentEfforts { get; }

        /// <summary>
        /// Clubs endpoint
        /// </summary>
        public ClubClient Clubs { get; }

        public async Task<bool> Deauthorize(string accessToken)
        {
            var request = new RestRequest("/oauth/deauthorize", Method.POST);
            request.AddQueryParameter("access_token", accessToken);
            var response = await RestClient.Execute(request);
            return response.Content?.Contains(accessToken) ?? false;
        }
    }
}
