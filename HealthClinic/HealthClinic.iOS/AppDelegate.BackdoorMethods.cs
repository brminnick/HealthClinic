#if DEBUG
using Foundation;
using HealthClinic.Shared;

namespace HealthClinic.iOS
{
    public partial class AppDelegate
    {
        public AppDelegate() => Xamarin.Calabash.Start();

        [Export(BackdoorMethodConstants.PostTestImageToAPI + ":")]
        public async void PostTestImageToAPI(NSString unusedString) => await UITestBackdoorMethodServices.PostTestImageToAPI().ConfigureAwait(false);

        [Export(BackdoorMethodConstants.DeleteTestFoodFromAPI + ":")]
        public async void DeleteTestFoodFromAPI(NSString unusedString) => await UITestBackdoorMethodServices.DeleteTestFoodFromAPI().ConfigureAwait(false);

        [Export(BackdoorMethodConstants.InjectImageIntoAddFoodPage + ":")]
        public void InjectImageIntoAddFoodPage(NSString unusedString) => UITestBackdoorMethodServices.InjectImageIntoAddFoodPage();
    }
}
#endif
