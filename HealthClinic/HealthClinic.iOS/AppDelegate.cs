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
        public async void PostTestImageToAPI(NSString unusedString) => await UITestBackdoorMethodServices.PostTestImageToAPI().ConfigureAwait(false);

        [Export("deleteTestFoodFromAPI:")]
        public async void DeleteTestFoodFromAPI(NSString unusedString) => await UITestBackdoorMethodServices.DeleteTestFoodFromAPI().ConfigureAwait(false);
        
        [Export("injectImageIntoAddFoodPage:")]
        public void InjectImageIntoAddFoodPage(NSString unusedString) => UITestBackdoorMethodServices.InjectImageIntoAddFoodPage();
#endif

        #endregion
    }
}
