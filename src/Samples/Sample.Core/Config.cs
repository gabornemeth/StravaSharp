using StravaSharp;

namespace Sample
{
    public class Config
    {
        public static string ClientId => "";
        public static string ClientSecret => "";

        public static StravaClient CreateOAuth2Cient(string redirectUrl)
        {
            var config = new RestSharp.Portable.OAuth2.Configuration.RuntimeClientConfiguration
            {
                IsEnabled = false,
                ClientId = Config.ClientId,
                ClientSecret = Config.ClientSecret,
                RedirectUri = redirectUrl,
                Scope = "write,view_private",
            };
            return new StravaClient(new Authentication.RequestFactory(), config);
        }
    }
}