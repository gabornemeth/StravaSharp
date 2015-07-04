using RestSharp.Portable;
using RestSharp.Portable.Authenticators;
using RestSharp.Portable.Authenticators.OAuth2;
using RestSharp.Portable.Authenticators.OAuth2.Client;
using RestSharp.Portable.Authenticators.OAuth2.Infrastructure;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace StravaSharp
{
    public class RestSharpAuthenticator : OAuth2Authenticator
    {
        public RestSharpAuthenticator(StravaClient client) : base(client)
        {
        }

        public override async Task Authenticate(IRestClient client, IRestRequest request)
        {
            if (!string.IsNullOrEmpty(Client.AccessToken))
                request.Parameters.Add(new Parameter { Name = "access_token", Value = Client.AccessToken, Type = ParameterType.GetOrPost });
        }
    }
}
