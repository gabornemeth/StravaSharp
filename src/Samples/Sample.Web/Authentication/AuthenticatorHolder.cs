namespace Sample.Web.Authentication
{
    public interface IAuthenticatorHolder
    {
        Authenticator Authenticator { get; set; }
    }

    public class AuthenticatorHolder : IAuthenticatorHolder
    {
        public Authenticator Authenticator { get; set; }
    }
}
