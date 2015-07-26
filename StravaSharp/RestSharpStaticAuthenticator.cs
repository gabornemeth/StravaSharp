using System;
using RestSharp.Portable;

namespace StravaSharp
{
    public class RestSharpStaticAuthenticator : RestSharp.Portable.Authenticators.IAuthenticator
    {
        public string AccessToken { get; set; }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            if (!string.IsNullOrEmpty(AccessToken))
                request.AddHeader("Authorization", "Bearer " + AccessToken);
        }
    }
}
