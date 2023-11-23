using CommunityToolkit.Mvvm.Input;
using ReactiveUI;
using Sample.Mobile.Authentication;
using Sample.ViewModels;
using StravaSharp;

namespace Sample.Mobile.ViewModels
{
    public class MobileMainViewModel : MainViewModel
    {
        private readonly MobileAuthenticator _authenticator;

        public IRelayCommand AuthenticationCommand { get; }
  
        public IRelayCommand UpdateCommand { get; }

        public MobileMainViewModel(MobileAuthenticator authenticator) : base(new Client(authenticator))
        {
            AuthenticationCommand = new AsyncRelayCommand(AuthenticateAsync, () => !IsAuthenticated);
            UpdateCommand = new RelayCommand(UpdateIsAuthenticated);
            _authenticator = authenticator;
            _authenticator.WhenAnyValue(x => x.AccessToken)
                .Subscribe(_ => UpdateIsAuthenticated());
            UpdateIsAuthenticated();
        }

        private async Task AuthenticateAsync()
        {
            await _authenticator.Authenticate();
            UpdateIsAuthenticated();
        }

        private void UpdateIsAuthenticated()
        {
            IsAuthenticated = _authenticator.IsAuthenticated;
            Status = IsAuthenticated ? "Authention sucessfull" : "Not Authenticated";
            OnPropertyChanged(nameof(IsAuthenticated));
            AuthenticationCommand.NotifyCanExecuteChanged();
        }
    }
}
