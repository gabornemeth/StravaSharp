using System;
using RestSharp.Portable;
using System.Threading.Tasks;

namespace StravaSharp
{
    public class RestSharpStaticAuthenticator : RestSharp.Portable.IAuthenticator
    {
        public string AccessToken { get; set; }

        #region IAuthenticator implementation

        public bool CanPreAuthenticate(IRestClient client, IRestRequest request, System.Net.ICredentials credentials)
        {
            return true;
        }

        public bool CanPreAuthenticate(IHttpClient client, IHttpRequestMessage request, System.Net.ICredentials credentials)
        {
            return false;
        }

        public bool CanHandleChallenge(IHttpClient client, IHttpRequestMessage request, System.Net.ICredentials credentials, IHttpResponseMessage response)
        {
            return false;
        }

        public System.Threading.Tasks.Task PreAuthenticate(IRestClient client, IRestRequest request, System.Net.ICredentials credentials)
        {
            return Task.Run(() =>
                {
                    if (!string.IsNullOrEmpty(AccessToken))
                        request.AddHeader("Authorization", "Bearer " + AccessToken);
                });
        }

        public System.Threading.Tasks.Task PreAuthenticate(IHttpClient client, IHttpRequestMessage request, System.Net.ICredentials credentials)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task HandleChallenge(IHttpClient client, IHttpRequestMessage request, System.Net.ICredentials credentials, IHttpResponseMessage response)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
