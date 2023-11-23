using Sample.Mobile.Authentication;

namespace Sample.Mobile
{
	public partial class LoginPage : ContentPage
	{
        private MobileAuthenticator _authenticator;

		public LoginPage (MobileAuthenticator authenticator)
		{
            _authenticator = authenticator ?? throw new ArgumentNullException(nameof(authenticator));
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var loginUri = _authenticator.GetLoginLinkUri();
            this.browser.Source = loginUri;
            this.browser.Navigated += Browser_Navigated;
        }

        private void Browser_Navigated(object sender, WebNavigatedEventArgs e)
        {
            _authenticator.OnPageLoaded(new Uri(e.Url));
        }
    }
}
