//
// TestAuthenticator.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2016, Gabor Nemeth
//

using RestSharp.Portable;
using System;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    /// <summary>
    /// Authenticator used for tests
    /// </summary>
    public class TestAuthenticator : RestSharp.Portable.IAuthenticator
    {
        public string AccessToken { get; private set; }

        public TestAuthenticator(string accessToken)
        {
            AccessToken = accessToken;
        }

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

        public Task PreAuthenticate(IRestClient client, IRestRequest request, System.Net.ICredentials credentials)
        {
            return Task.Run(() =>
            {
                if (!string.IsNullOrEmpty(AccessToken))
                    request.AddHeader("Authorization", "Bearer " + AccessToken);
            });
        }

        public Task PreAuthenticate(IHttpClient client, IHttpRequestMessage request, System.Net.ICredentials credentials)
        {
            throw new NotImplementedException();
        }

        public Task HandleChallenge(IHttpClient client, IHttpRequestMessage request, System.Net.ICredentials credentials, IHttpResponseMessage response)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
