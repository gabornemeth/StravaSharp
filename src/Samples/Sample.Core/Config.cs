using System;
using Sample.Core.Authentication;

namespace Sample
{
    public class Config
    {
        //TODO: Add your clientId and Secret from your strava account: https://www.strava.com/settings/api
        public static string ClientId => "";
        public static string ClientSecret => "";
        public static string RedirectUrl => "";

        public static StravaClient CreateOAuth2Cient(Action<OAuth2ClientConfiguration> customize = null)
        {
            var config = new OAuth2ClientConfiguration
            {
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                RedirectUri = RedirectUrl,
                Scope = "activity:read_all,profile:read_all"
            };
            customize?.Invoke(config);
            return new StravaClient(config);
        }
    }
}
