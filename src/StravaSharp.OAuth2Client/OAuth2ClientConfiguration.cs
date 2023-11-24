namespace StravaSharp.OAuth2Client;

public class OAuth2ClientConfiguration
{
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public required string RedirectUri { get; set; }
    public string? Scope { get; set; }
}
