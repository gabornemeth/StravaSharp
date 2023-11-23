//
// TestAuthenticator.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2016-2023, Gabor Nemeth
//

using RestSharp;
using System.Threading.Tasks;

namespace StravaSharp.Tests
{
    /// <summary>
    /// Authenticator used for tests
    /// </summary>
    public class TestAuthenticator : RestSharp.Authenticators.AuthenticatorBase
    {
        public TestAuthenticator(string accessToken) : base(accessToken)
        {
        }

        protected override ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            return new ValueTask<Parameter>(new HeaderParameter("Authorization", "Bearer " + Token));
        }
    }
}
