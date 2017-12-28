using RestSharp.Portable;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;
using RestSharp.Portable.OAuth2;
using RestSharp.Portable.OAuth2.Infrastructure;
using Xamarin.Forms;

namespace Sample.Mobile.Authentication
{
    /// <summary>
    /// OAuth 2 authenticator for Extrava
    /// This authenticator presents custom UI for the user to sign in.
    /// </summary>
    public class Authenticator : OAuth2Authenticator
    {
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
#if __ANDROID__
            if (uri.AbsoluteUri.StartsWith(Client.Configuration.RedirectUri, StringComparison.InvariantCulture))
#else
            if (uri.AbsoluteUri.StartsWith(Client.Configuration.RedirectUri))
#endif
            {
                Debug.WriteLine("Navigated to redirect url.");
                var parameters = uri.Query.Remove(0, 1).ParseQueryString(); // query portion of the response
                await Client.GetUserInfo(parameters);

                if (!string.IsNullOrEmpty(Client.AccessToken))
                {
                    AccessToken = Client.AccessToken;

                    // dismiss login page
                        await Application.Current.MainPage.Navigation.PopAsync();
                    return true;
                }
            }

            return false;
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

        public override bool CanPreAuthenticate(IRestClient client, IRestRequest request, ICredentials credentials)
        {
            return true;
        }

        public override bool CanPreAuthenticate(IHttpClient client, IHttpRequestMessage request, ICredentials credentials)
        {
            return false;
        }

        public override Task PreAuthenticate(IRestClient client, IRestRequest request, ICredentials credentials)
        {
            return Task.Run(() =>
            {
                if (!string.IsNullOrEmpty(AccessToken))
                    request.AddHeader("Authorization", "Bearer " + AccessToken);
            });
        }

        public override Task PreAuthenticate(IHttpClient client, IHttpRequestMessage request, ICredentials credentials)
        {
            throw new NotImplementedException();
        }
    }
}
