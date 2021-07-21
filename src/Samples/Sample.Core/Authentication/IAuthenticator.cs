using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Core.Authentication
{
    public interface IAuthenticator
    {
        bool IsAuthenticated { get; }
        string AccessToken { get; }
    }
}
