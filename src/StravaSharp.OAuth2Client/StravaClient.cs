using IdentityModel;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace StravaSharp.OAuth2Client;

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
        ExtractTokensFromTokenResponse(response);
    }

    public override async Task UpdateAccessToken(string? refreshToken = null)
    {
        if (RefreshToken == null && refreshToken == null)
        {
            throw new InvalidOperationException("Cannot update access token without refreshtoken");
        }

        var client = new RestClient();
        var request = new RestRequest(TokenUri, Method.Post);
        request.AddQueryParameter(OidcConstants.TokenRequest.ClientId, Configuration.ClientId);
        request.AddQueryParameter(OidcConstants.TokenRequest.ClientSecret, Configuration.ClientSecret);
        request.AddQueryParameter(OidcConstants.TokenRequest.RefreshToken, RefreshToken ?? refreshToken);
        request.AddQueryParameter(OidcConstants.TokenRequest.GrantType, OidcConstants.GrantTypes.RefreshToken);
        var response = await client.ExecuteAsync(request);
        ExtractTokensFromTokenResponse(response);
    }

    private void ExtractTokensFromTokenResponse(RestResponse response)
    {
        if (response.IsSuccessStatusCode)
        {
            var responseAsJson = JObject.Parse(response?.Content ?? "");
            AccessToken = responseAsJson?.GetValue(OidcConstants.TokenResponse.AccessToken)?.Value<string>();
            RefreshToken = responseAsJson?.GetValue(OidcConstants.TokenResponse.RefreshToken)?.Value<string>();
        }
    }
}
