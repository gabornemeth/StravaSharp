namespace Sample.Web.Authentication
{
    public interface IAuthenticatorHolder
    {
        WebAuthenticator Authenticator { get; set; }
    }

    public class AuthenticatorHolder : IAuthenticatorHolder
    {
        public WebAuthenticator Authenticator { get; set; }
    }
}
