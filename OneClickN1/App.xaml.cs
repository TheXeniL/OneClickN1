using FFImageLoading;
using FFImageLoading.Config;
using Xamarin.Forms;

namespace OneClickN1
{
    public partial class App : Application
    {

        public static void Initialize(Configuration config) { }

		public App()
        {
			ImageService.Instance.InvalidateMemoryCache();
			InitializeComponent();
            MainPage = new NavigationPage (new OneClickN1Page()){ BarTextColor = Color.White };

			if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
			{
				var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                AppResources.AppResources.Culture = ci; // set the RESX for resource localization
				DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
			}

        }

        protected override void OnStart()
        {
			// Handle when your app starts
			ImageService.Instance.InvalidateMemoryCache();
		}

        protected override void OnSleep()
        {
			// Handle when your app sleeps
            ImageService.Instance.InvalidateMemoryCache();
		}

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
