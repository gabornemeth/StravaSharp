using IdentityModel;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.Core.Authentication
{
    public class OAuth2ClientConfiguration
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string Scope { get; set; }
    }

    public class StravaClient : OAuth2Client
    {
        private string BaseUri => "https://www.strava.com";

        public override string AuthorizeUri => $"{BaseUri}/oauth/authorize";

        public override string TokenUri => $"{BaseUri}/oauth/token";

        public StravaClient(OAuth2ClientConfiguration config) : base(config)
        {
        }

        protected override void CustomizeAuthorizationUrlRequest(RestRequest request)
        {
            base.CustomizeAuthorizationUrlRequest(request);
            request.AddParameter("response_type", "code");
            request.AddParameter("approval_prompt", "auto");
        }

        public override async Task Authorize(IDictionary<string, string> redirectUrlParameters)
        {
            // Strava needs parameters in query string, so can't leverage IdentityModel too much here.
            var client = new RestClient();
            var request = new RestRequest(TokenUri, Method.Post);
            request.AddQueryParameter(OidcConstants.TokenRequest.ClientId, Configuration.ClientId);
            request.AddQueryParameter(OidcConstants.TokenRequest.ClientSecret, Configuration.ClientSecret);
            request.AddQueryParameter(OidcConstants.TokenRequest.Code, redirectUrlParameters["code"]);
            request.AddQueryParameter(OidcConstants.TokenRequest.GrantType, OidcConstants.GrantTypes.AuthorizationCode);
            var response = await client.ExecuteAsync(request);
            var responseAsJson = JObject.Parse(response.Content);
            AccessToken = responseAsJson.GetValue(OidcConstants.TokenResponse.AccessToken).Value<string>();
        }
    }

    public abstract class OAuth2Client
    {
        private readonly OAuth2ClientConfiguration _config;

        public string AccessToken { get; protected set; }

        public abstract string AuthorizeUri { get; }

        public abstract string TokenUri { get; }

        public OAuth2ClientConfiguration Configuration => _config;

        public OAuth2Client(OAuth2ClientConfiguration config)
        {
            _config = config;
        }

        public string GetAuthorizationUrl()
        {
            var client = new RestClient();
            
            var request = new RestRequest(new Uri(AuthorizeUri));
            request.AddParameter(OidcConstants.AuthorizeRequest.ClientId, _config.ClientId);
            request.AddParameter(OidcConstants.AuthorizeRequest.RedirectUri, _config.RedirectUri);
            request.AddParameter(OidcConstants.AuthorizeRequest.Scope, _config.Scope);
            CustomizeAuthorizationUrlRequest(request);
            var authorizationUri = RestSharp.BuildUriExtensions.BuildUri(client, request);
            return authorizationUri.ToString();
        }

        protected virtual void CustomizeAuthorizationUrlRequest(RestRequest request)
        {
        }

        public abstract Task Authorize(IDictionary<string, string> redirectUrlParameters);
    }
}
