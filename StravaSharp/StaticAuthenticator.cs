using RestSharp.Portable;
using System;

namespace StravaSharp
{
    /// <summary>
    /// Static authenticator - mainly used for testing
    /// </summary>
    public class StaticAuthenticator : IAuthenticator
    {
        private class RestSharpStaticAuthenticator : RestSharp.Portable.Authenticators.IAuthenticator
        {
            private string _accessToken;

            public RestSharpStaticAuthenticator(string accessToken)
            {
                _accessToken = accessToken;
            }

            public void Authenticate(IRestClient client, IRestRequest request)
            {
                if (!string.IsNullOrEmpty(_accessToken))
                    request.AddHeader("Authorization", "Bearer " + _accessToken);
                //request.Parameters.Add(new Parameter { Name = "access_token", Value = _accessToken, Type = ParameterType.HttpHeader });
            }
        }

        private RestSharpStaticAuthenticator _authenticator;

        public StaticAuthenticator(string accessToken)
        {
            AccessToken = accessToken;
            _authenticator = new RestSharpStaticAuthenticator(accessToken);
        }

        public string AccessToken
        {
            get;
            private set;
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public RestSharp.Portable.Authenticators.IAuthenticator RestSharpAuthenticator
        {
            get
            {
                return _authenticator;
            }
        }

        public void Authenticate()
        {
            // already authenticated - got the access token
        }
    }
}
