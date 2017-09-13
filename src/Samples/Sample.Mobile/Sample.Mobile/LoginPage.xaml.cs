using Sample.Mobile.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sample.Mobile
{
	//[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        private Authenticator _authenticator;

		public LoginPage (Authenticator authenticator)
		{
            _authenticator = authenticator ?? throw new ArgumentNullException(nameof(authenticator));
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var loginUri = await _authenticator.GetLoginLinkUri();
        }
    }
}