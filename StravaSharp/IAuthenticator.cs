using System;
using System.Threading.Tasks;

namespace StravaSharp
{
    public interface IAuthenticator
    {
        /// <summary>
        /// The access token received from Strava.
        /// </summary>
        string AccessToken { get; set; }

        /// <summary>
        /// Returns if the client has been successfully authenticated.
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Authenticate the client
        /// </summary>
        Task Authenticate();

        RestSharp.Portable.IAuthenticator RestSharpAuthenticator { get; }
    }
}
