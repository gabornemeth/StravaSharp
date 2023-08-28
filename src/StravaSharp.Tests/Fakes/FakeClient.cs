using Moq;
using Newtonsoft.Json;
using RestSharp.Portable;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    internal class FakeClient : Client
    {
        protected override IRestClient RestClient { get; } = new FakeRestClient();

        public FakeClient(IAuthenticator authenticator) : base(authenticator)
        {
        }
    }

    class FakeRestClient : IRestClient
    {
        public IAuthenticator Authenticator { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Uri BaseUrl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IParameterCollection DefaultParameters => throw new NotImplementedException();

        public CookieContainer CookieContainer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IWebProxy Proxy { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICredentials Credentials { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IgnoreResponseStatusCode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TimeSpan? Timeout { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserAgent { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IDictionary<string, IDeserializer> ContentHandlers => throw new NotImplementedException();

        public IDictionary<string, IEncoding> EncodingHandlers => throw new NotImplementedException();

        public IRestClient AddEncoding(string encodingId, IEncoding encoding)
        {
            throw new NotImplementedException();
        }

        public IRestClient AddHandler(string contentType, IDeserializer deserializer)
        {
            throw new NotImplementedException();
        }

        public IRestClient ClearEncodings()
        {
            throw new NotImplementedException();
        }

        public IRestClient ClearHandlers()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse> Execute(IRestRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse<T>> Execute<T>(IRestRequest request)
        {
            string json = null;
            var response = new Mock<IRestResponse<T>>();
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
                json = GetFakeResponse("activities_id_efforts");
            }

            if (json == null)
            {
                response.Setup(x => x.StatusCode).Returns(HttpStatusCode.NotFound);
            }
            else
            {
                response.Setup(x => x.Content).Returns(json);
                response.Setup(x => x.Data)
                    .Returns(JsonConvert.DeserializeObject<T>(json));
            }

            return Task.FromResult(response.Object);
        }

        public Task<IRestResponse> Execute(IRestRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<IRestResponse<T>> Execute<T>(IRestRequest request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public IEncoding GetEncoding(IEnumerable<string> encodingIds)
        {
            throw new NotImplementedException();
        }

        public IDeserializer GetHandler(string contentType)
        {
            throw new NotImplementedException();
        }

        public IRestClient RemoveEncoding(string encodingId)
        {
            throw new NotImplementedException();
        }

        public IRestClient RemoveHandler(string contentType)
        {
            throw new NotImplementedException();
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
