using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using Xamarin.Forms;
using RestSharp.Authenticators;
using RestSharp;
using Sample.Core.Authentication;
using Sample.Core;

namespace Sample.Mobile.Authentication
{
    /// <summary>
    /// OAuth 2 authenticator for StravaSharp mobile samples
    /// This authenticator presents custom UI for the user to sign in.
    /// </summary>
    public class MobileAuthenticator : IAuthenticator
    {
        private readonly OAuth2Client _client;

        /// <summary>
        /// The access token that was received from the server.
        /// </summary>
        public string AccessToken
        {
            get; set;
        }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(AccessToken); }
        }

        public MobileAuthenticator(OAuth2Client client)
        {
            _client = client;
        }

        public Uri GetLoginLinkUri()
        {
            var uri = _client.GetAuthorizationUrl();
            return new Uri(uri);
        }

        public async Task<bool> OnPageLoaded(Uri uri)
        {
            try
            {
#if __ANDROID__
            if (uri.AbsoluteUri.StartsWith(_client.Configuration.RedirectUri, StringComparison.InvariantCulture))
#else
                if (uri.AbsoluteUri.StartsWith(_client.Configuration.RedirectUri))
#endif
                {
                    Debug.WriteLine("Navigated to redirect url.");
                    var parameters = uri.Query.ParseQueryString();

                    await _client.Authorize(parameters);

                    if (!string.IsNullOrEmpty(_client.AccessToken))
                    {
                        AccessToken = _client.AccessToken;
                        // dismiss login page
                        await Application.Current.MainPage.Navigation.PopAsync();
                        return true;
                    }
                }

                return false;
            }
            catch ( Exception ex)
            {
                throw ex;
            }
        }

        public async Task Authenticate()
        {
            if (IsAuthenticated)
                return;

            // Not authenticated yet
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new LoginPage(this));
            }
            catch (Exception ex)
            {
                //
                // Bad Parameter, SSL/TLS Errors and Network Unavailable errors are to be handled here.
                //
                Debug.WriteLine(ex.ToString());
            }
        }

        public ValueTask Authenticate(IRestClient client, RestRequest request)
        {
            if (!string.IsNullOrEmpty(AccessToken))
                request.AddHeader("Authorization", "Bearer " + AccessToken);
            return new ValueTask();
        }
    }
}
