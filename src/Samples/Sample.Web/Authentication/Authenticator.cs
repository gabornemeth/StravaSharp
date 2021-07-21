using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp.Portable;
using RestSharp.Portable.OAuth2;
using RestSharp.Portable.OAuth2.Infrastructure;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Sample.Web
{
    public class Authenticator : OAuth2Authenticator, IAuthenticator
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// The access token that was received from the server.
        /// </summary>
        public string AccessToken {
            get {
                return _httpContextAccessor.HttpContext.Session.GetString("AccessToken");
            }
            set {
                _httpContextAccessor.HttpContext.Session.SetString("AccessToken", value);
            }
        }

        public bool IsAuthenticated => AccessToken != null;

        public Authenticator(OAuth2Client client, IHttpContextAccessor httpContextAccessor) : base(client)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
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