using StravaSharp;

namespace Sample
{
    public class Config
    {
        //TODO: Add your clientId and Secret from your strava account: https://www.strava.com/settings/api
        public static string ClientId => "xxx";
        public static string ClientSecret => "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        public static string RedirectUrl => $"http://strava.ballendat.com/";

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