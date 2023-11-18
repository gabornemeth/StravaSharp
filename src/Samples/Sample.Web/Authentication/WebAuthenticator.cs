using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Sample.Core.Authentication;
using RestSharp.Authenticators;
using RestSharp;
using System.Linq;
using Microsoft.AspNetCore.WebUtilities;

namespace Sample.Web
{
    /// <summary>
    /// Web authenticator
    /// </summary>
    public class WebAuthenticator : IAuthenticator
    {
        private readonly StravaClient _client;
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

        public WebAuthenticator(StravaClient client, IHttpContextAccessor httpContextAccessor)
        {
            _client = client;
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public Uri GetLoginLinkUri()
        {
            var uri = _client.GetAuthorizationUrl();
            return new Uri(uri);
        }

        public async Task<bool> OnPageLoaded(Uri uri)
        {
            if (uri.AbsoluteUri.StartsWith(_client.Configuration.RedirectUri))
            {
                Debug.WriteLine("Navigated to redirect url.");

                var parameters = QueryHelpers.ParseQuery(uri.Query)
                    .ToDictionary(x => x.Key, x => string.Concat(x.Value));
                
                await _client.Authorize(parameters);

                if (!string.IsNullOrEmpty(_client.AccessToken))
                {
                    AccessToken = _client.AccessToken;
                    return true;
                }
            }

            return false;
        }

        public ValueTask Authenticate(IRestClient client, RestRequest request)
        {
            if (!string.IsNullOrEmpty(AccessToken))
                request.AddHeader("Authorization", "Bearer " + AccessToken);
            return ValueTask.CompletedTask;
        }
    }
}