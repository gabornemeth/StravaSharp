using CommunityToolkit.Mvvm.Input;
using Sample.Mobile.Authentication;
using Sample.ViewModels;
using StravaSharp;
using System.Threading.Tasks;

namespace Sample.Mobile.ViewModels
{
    public class MobileMainViewModel : MainViewModel
    {
        private static readonly MobileAuthenticator auth = CreateAuthenticator();

        public MobileMainViewModel() : base(new Client(auth))
        {
            UpdateIsAuthenticated();
        }

        static MobileAuthenticator CreateAuthenticator()
        {
            var client = Config.CreateOAuth2Cient();
            return new MobileAuthenticator(client);
        }

        private IRelayCommand _authCommand;
        
        public IRelayCommand GetAuthenticationCommand
            => _authCommand ??= new AsyncRelayCommand(AuthenticateAsync, () => !IsAuthenticated);

        private async Task AuthenticateAsync()
        {
            await auth.Authenticate();
            UpdateIsAuthenticated();
        }

        private IRelayCommand _updateCommand;
        
        public IRelayCommand GetUpdateCommand
            => _updateCommand ??= new RelayCommand(UpdateIsAuthenticated);
        
        private void UpdateIsAuthenticated()
        {
            IsAuthenticated = auth.IsAuthenticated;
            Status = IsAuthenticated ? "Authention sucessfull" : "Not Authenticated";
            OnPropertyChanged(nameof(IsAuthenticated));
            GetAuthenticationCommand.NotifyCanExecuteChanged();
        }
    }
}
