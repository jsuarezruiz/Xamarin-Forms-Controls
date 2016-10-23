using WrapViewSample.ViewModels.Base;
using WrapViewSample.Views;
using Xamarin.Forms;

namespace WrapView
{
    public partial class App : Application
    {
        private static ViewModelLocator _locator;

        public static ViewModelLocator Locator
        {
            get { return _locator = _locator ?? new ViewModelLocator(); }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MonkeysView(null));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
