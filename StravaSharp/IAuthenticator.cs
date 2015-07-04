using System;

namespace StravaSharp
{
    public interface IAuthenticator
    {
        /// <summary>
        /// The access token received from Strava.
        /// </summary>
        string AccessToken { get; }

        /// <summary>
        /// Returns if the client has been successfully authenticated.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Authenticate the client
        /// </summary>
        void Authenticate();

        RestSharp.Portable.Authenticators.IAuthenticator RestSharpAuthenticator { get; }
    }
}
