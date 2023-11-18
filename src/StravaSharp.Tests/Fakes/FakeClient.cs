using Moq;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    internal class FakeClient : Client
    {
        private readonly FakeRestClient _restClient;

        protected override IRestClient RestClient { get; }

        public FakeClient(IAuthenticator authenticator) : base(authenticator)
        {
            _restClient = new FakeRestClient();
            RestClient = _restClient.Object;
        }
    }

    class FakeRestClient : Mock<IRestClient>
    {

        public FakeRestClient()
        {
            this.Setup(x => x.ExecuteAsync(It.IsAny<RestRequest>(), It.IsAny<CancellationToken>()))
                .Returns<RestRequest, CancellationToken>((request, token) =>
                {
                    string json = null;
                    var response = new RestResponse { Request = request };
                    if (request.Resource == "/api/v3/athlete/activities")
                    {
                        json = GetFakeResponse("athlete_activities");
                    }
                    else if (request.Resource == "/api/v3/activities/{id}/zones")
                    {
                        json = GetFakeResponse("activities_zones");
                    }
                    else if (request.Resource == "/api/v3/activities/{id}")
                    {
                        if (request.Method == Method.Get)
                        {
                            json = GetFakeResponse("activities_id_efforts");
                        }
                        else if (request.Method == Method.Put)
                        {
                            json = GetFakeResponse("update_activity");
                        }
                    }
                    else if (request.Resource.StartsWith("/api/v3/segment_efforts"))
                    {
                        json = GetFakeResponse("segment_efforts");
                    }

                    if (json == null)
                    {
                        response.StatusCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        response.Content = json;
                    }

                    return Task.FromResult(response);
                });
        }

        private string GetFakeResponse(string name)
        {
            var resourceName = Assembly.GetExecutingAssembly().GetManifestResourceNames()
                .FirstOrDefault(x => x.Contains($"{name}.json"));
            using var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName));
            return reader.ReadToEnd();
        }
    }
}
