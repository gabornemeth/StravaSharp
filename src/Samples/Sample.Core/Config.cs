using StravaSharp;

namespace Sample
{
    public class Config
    {
        //TODO: Add your clientId and Secret from your strava account: https://www.strava.com/settings/api
        public static string ClientId => "xxx";
        public static string ClientSecret => "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        public static string RedirectUrl => $"xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

        public static StravaClient CreateOAuth2Cient()
        {
            var config = new RestSharp.Portable.OAuth2.Configuration.RuntimeClientConfiguration
            {
                IsEnabled = false,
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                RedirectUri = RedirectUrl,
                Scope = "write,view_private",
            };
            return new StravaClient(new Authentication.RequestFactory(), config);
        }
    }
}