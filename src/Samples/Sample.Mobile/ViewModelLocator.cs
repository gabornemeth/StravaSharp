using CommunityToolkit.Mvvm.DependencyInjection;
using Sample.Mobile.Authentication;
using Sample.Mobile.ViewModels;
using Splat;
using StravaSharp;

namespace Sample.Mobile
{
    class ServiceCollection : IServiceProvider
    {
        ModernDependencyResolver _container = new ModernDependencyResolver();

        public ServiceCollection()
        {
            _container.Register<MobileMainViewModel>(() =>
                {
                    var client = Config.CreateOAuth2Cient();
                    var authenticator = new MobileAuthenticator(client);
                    return new MobileMainViewModel(authenticator);
                });
        }

        public object GetService(Type serviceType)
        {
            return _container.GetService(serviceType);
        }
    }

    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            Ioc.Default.ConfigureServices(new ServiceCollection());
        }

        public MobileMainViewModel MainViewModel => Ioc.Default.GetService<MobileMainViewModel>();

        private static ViewModelLocator _locator;

        public static ViewModelLocator Instance
        {
            get
            {
                if (_locator == null)
                {
                    if (Application.Current.Resources.ContainsKey("Locator"))
                    {
                        _locator = (ViewModelLocator)Application.Current.Resources["Locator"];
                    }
                    else
                    {
                        _locator = new ViewModelLocator();
                    }
                }
                return _locator;
            }
        }
    }
}
