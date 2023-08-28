using System;
using StravaSharp;

namespace Sample
{
    public class Config
    {
        //TODO: Add your clientId and Secret from your strava account: https://www.strava.com/settings/api
        public static string ClientId => "xxx";
        public static string ClientSecret => "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
        public static string RedirectUrl => $"xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";

        public static StravaClient CreateOAuth2Cient(Action<RestSharp.Portable.OAuth2.Configuration.RuntimeClientConfiguration> customize = null)
        {
            var config = new RestSharp.Portable.OAuth2.Configuration.RuntimeClientConfiguration
            {
                IsEnabled = false,
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                RedirectUri = RedirectUrl,
                Scope = "activity:read_all,activity:write,profile:read_all,profile:write"
            };
            customize?.Invoke(config);
            return new StravaClient(new Authentication.RequestFactory(), config);
        }
    }
}