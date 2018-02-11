using GalaSoft.MvvmLight.Ioc;
using Sample.Mobile.ViewModels;
using Xamarin.Forms;

namespace Sample.Mobile
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            SimpleIoc.Default.Register<MobileMainViewModel>();
        }

        public MobileMainViewModel MainViewModel => SimpleIoc.Default.GetInstance<MobileMainViewModel>();

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
