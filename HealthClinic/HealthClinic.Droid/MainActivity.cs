using System.Threading.Tasks;

using Android.OS;
using Android.App;
using Android.Content.PM;

using Plugin.Permissions;

using Java.Interop;

namespace HealthClinic.Droid
{
    [Activity(Label = "HealthClinic.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        #region BackdoorMethods
#if DEBUG
        [Export(nameof(PostTestImageToAPI))]
        public async void PostTestImageToAPI() => await UITestBackdoorMethodServices.PostTestImageToAPI().ConfigureAwait(false);

        [Export(nameof(DeleteTestFoodFromAPI))]
        public async void DeleteTestFoodFromAPI() => await UITestBackdoorMethodServices.DeleteTestFoodFromAPI().ConfigureAwait(false);

        [Export(nameof(InjectImageIntoAddFoodPage))]
        public void InjectImageIntoAddFoodPage() => UITestBackdoorMethodServices.InjectImageIntoAddFoodPage();
#endif

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            FFImageLoading.Forms.Platform.CachedImageRenderer.InitImageViewHandler();

            LoadApplication(new App());
        }
    }
}
