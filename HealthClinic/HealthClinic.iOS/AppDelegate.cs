using System.Threading.Tasks;

using UIKit;
using Foundation;

namespace HealthClinic.iOS
{
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            global::Xamarin.Forms.Forms.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

#if DEBUG
            Xamarin.Calabash.Start();
#endif

            LoadApplication(new App());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }

        #region BackdoorMethods
#if DEBUG
        [Export("postTestImageToAPI:")]
        public void PostTestImageToAPI(NSString unusedString) => UITestBackdoorMethodServices.PostTestImageToAPI().GetAwaiter();

        [Export("deleteTestFoodFromAPI:")]
        public void DeleteTestFoodFromAPI(NSString unusedString) => UITestBackdoorMethodServices.DeleteTestFoodFromAPI().GetAwaiter();

        [Export("injectImageIntoAddFoodPage:")]
        public void InjectImageIntoAddFoodPage(NSString unusedString) => UITestBackdoorMethodServices.InjectImageIntoAddFoodPage();
#endif

        #endregion
    }
}
