using RestSharp.Portable;
using System;
using System.Threading.Tasks;

namespace StravaSharp
{
    /// <summary>
    /// Static authenticator - mainly used for testing
    /// </summary>
    public class StaticAuthenticator : IAuthenticator
    {
        private RestSharpStaticAuthenticator _authenticator;

        public StaticAuthenticator(string accessToken)
        {
            _authenticator = new RestSharpStaticAuthenticator();
            AccessToken = accessToken;
        }

        private string _accessToken;
        public string AccessToken
        {
            get { return _accessToken; }
            set
            {
                if (_accessToken != value)
                {
                    _accessToken = value;
                    _authenticator.AccessToken = _accessToken;
                }
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        public RestSharp.Portable.IAuthenticator RestSharpAuthenticator
        {
            get
            {
                return _authenticator;
            }
        }

        public System.Threading.Tasks.Task Authenticate()
        {
            // already authenticated - got the access token
            return Task.Run(() => { });
        }
    }
}
