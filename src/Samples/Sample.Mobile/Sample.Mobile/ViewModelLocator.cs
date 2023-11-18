using CommunityToolkit.Mvvm.DependencyInjection;
using Sample.Mobile.ViewModels;
using Splat;
using System;
using Xamarin.Forms;

namespace Sample.Mobile
{
    class ServiceCollection : IServiceProvider
    {
        Splat.ModernDependencyResolver _container = new Splat.ModernDependencyResolver();

        public ServiceCollection()
        {
            _container.Register<MobileMainViewModel, MobileMainViewModel>();
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
                        _locator = (ViewModelLocator) Application.Current.Resources["Locator"];
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
