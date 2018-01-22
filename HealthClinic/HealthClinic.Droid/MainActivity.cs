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
        [Export("PostTestImageToAPI")]
        public void PostTestImageToAPI() =>
            Task.Run(async () => await UITestBackdoorMethodServices.PostTestImageToAPI()).GetAwaiter().GetResult();

        [Export("DeleteTestFoodFromAPI")]
        public void DeleteTestFoodFromAPI() =>
            Task.Run(async () => await UITestBackdoorMethodServices.DeleteTestFoodFromAPI()).GetAwaiter().GetResult();

        [Export("InjectImageIntoAddFoodPage")]
        public void InjectImageIntoAddFoodPage() => UITestBackdoorMethodServices.InjectImageIntoAddFoodPage();
#endif

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FFImageLoading.Forms.Droid.CachedImageRenderer.Init(true);

            LoadApplication(new App());
        }
    }
}
