using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using RestSharp.Portable;
using RestSharp.Portable.OAuth2;
using RestSharp.Portable.OAuth2.Infrastructure;
using System.Diagnostics;

namespace Sample.Web
{
    public class Authenticator : RestSharp.Portable.OAuth2.OAuth2Authenticator
    {
        /// <summary>
        /// The access token that was received from the server.
        /// </summary>
        public string AccessToken
        {
            get
            {
                var accessToken = HttpContext.Current.Session["AccessToken"];
                return accessToken == null ? null : accessToken as string;
            }
            set
            {
                HttpContext.Current.Session["AccessToken"] = value;
            }
        }

        public bool IsAuthenticated => AccessToken != null;

        public Authenticator(OAuth2Client client) : base(client)
        {
        }

        public async Task<Uri> GetLoginLinkUri()
        {
            var uri = await Client.GetLoginLinkUri();
            return new Uri(uri);
        }

        public async Task<bool> OnPageLoaded(Uri uri)
        {
            if (uri.AbsoluteUri.StartsWith(Client.Configuration.RedirectUri))
            {
                Debug.WriteLine("Navigated to redirect url.");
                var parameters = uri.Query.Remove(0, 1).ParseQueryString(); // query portion of the response
                await Client.GetUserInfo(parameters);

                if (!string.IsNullOrEmpty(Client.AccessToken))
                {
                    AccessToken = Client.AccessToken;
                    return true;
                }
            }

            return false;
        }

        public override bool CanPreAuthenticate(IRestClient client, IRestRequest request, ICredentials credentials)
        {
            return true;
        }

        public override bool CanPreAuthenticate(IHttpClient client, IHttpRequestMessage request, ICredentials credentials)
        {
            return false;
        }

        public override async Task PreAuthenticate(IRestClient client, IRestRequest request, ICredentials credentials)
        {
            if (!string.IsNullOrEmpty(AccessToken))
                request.AddHeader("Authorization", "Bearer " + AccessToken);
        }

        public override Task PreAuthenticate(IHttpClient client, IHttpRequestMessage request, ICredentials credentials)
        {
            throw new NotImplementedException();
        }
    }
}