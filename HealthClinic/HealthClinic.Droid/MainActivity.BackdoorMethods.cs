#if DEBUG
using HealthClinic.Shared;
using Java.Interop;

namespace HealthClinic.Droid
{
    public partial class MainActivity
    {
        [Export(BackdoorMethodConstants.PostTestImageToAPI)]
        public async void PostTestImageToAPI() => await UITestBackdoorMethodServices.PostTestImageToAPI().ConfigureAwait(false);

        [Export(BackdoorMethodConstants.DeleteTestFoodFromAPI)]
        public async void DeleteTestFoodFromAPI() => await UITestBackdoorMethodServices.DeleteTestFoodFromAPI().ConfigureAwait(false);

        [Export(BackdoorMethodConstants.InjectImageIntoAddFoodPage)]
        public void InjectImageIntoAddFoodPage() => UITestBackdoorMethodServices.InjectImageIntoAddFoodPage();
    }
}
#endif
