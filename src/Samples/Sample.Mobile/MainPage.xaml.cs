namespace Sample.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
	{
        public MainPage()
		{
			InitializeComponent();            
		}

	    protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        
            ViewModelLocator.Instance.MainViewModel.UpdateCommand.Execute(null);
        }
	}
}
