using FFImageLoading.Forms.Touch;
using Foundation;
using UIKit;
using ImageCircle.Forms.Plugin.iOS;
using FFImageLoading;

namespace OneClickN1.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
			CachedImageRenderer.Init();
            ImageCircleRenderer.Init();

			LoadApplication(new App());
			ImageService.Instance.InvalidateMemoryCache();


			return base.FinishedLaunching(app, options);
        }
    }
}
