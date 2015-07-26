using System;
using System.Collections.Generic;
using System.Text;

namespace StravaSharp
{
    public interface IOAuth2Authenticator : IAuthenticator
    {
        event EventHandler<TokenReceivedEventArgs> AccessTokenReceived;
    }

    public class TokenReceivedEventArgs : System.EventArgs
    {
        /// <summary>
        /// Access token.
        /// </summary>
        public String Token { get; private set; }

        /// <summary>
        /// Initializes a new instance of the TokenReceivedEventArgs class.
        /// </summary>
        /// <param name="token">The token received from the server.</param>
        public TokenReceivedEventArgs(string token)
        {
            Token = token;
        }
    }
}
