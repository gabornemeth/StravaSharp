using IdentityModel;
using IdentityModel.Client;
using RestSharp;

namespace StravaSharp.OAuth2Client;

public abstract class OAuth2Client
{
    private readonly OAuth2ClientConfiguration _config;

    public string? AccessToken { get; protected set; }

    public string? RefreshToken { get; protected set; }

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

    public virtual async Task UpdateAccessToken(string? refreshToken = null)
    {
        var refreshTokenToUse = (refreshToken ?? RefreshToken) ?? throw new InvalidOperationException("Cannot update access token without refreshtoken");
        var client = new HttpClient();
        var response = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
        {
            Address = TokenUri,
            ClientId = Configuration.ClientId,
            ClientSecret = Configuration.ClientSecret,
            GrantType = OidcConstants.TokenRequest.RefreshToken,
            RefreshToken = refreshTokenToUse
        });
        AccessToken = response?.AccessToken;
        RefreshToken = response?.RefreshToken;
    }
}
