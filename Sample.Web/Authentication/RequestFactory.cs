using System;
using RestSharp.Portable;
using RestSharp.Portable.OAuth2.Infrastructure;
using RestSharp.Portable.HttpClient;

namespace Sample.Web.Authentication
{
    public class RequestFactory : IRequestFactory
    {
        #region IRequestFactory implementation

        public RestSharp.Portable.IRestClient CreateClient()
        {
            return new RestClient();
        }

        public RestSharp.Portable.IRestRequest CreateRequest(string resource)
        {
            return new RestRequest(resource);
        }

        #endregion
    }
}

