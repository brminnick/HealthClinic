using Xamarin.UITest;

using Xamarin.UITest.iOS;
using Xamarin.UITest.Android;

namespace HealthClinic.UITests
{
    public static class BackdoorMethodServices
    {
        internal static void PostTestImageToAPI(IApp app)
        {
            switch (app)
            {
                case iOSApp app_iOS:
                    app_iOS.Invoke("postTestImageToAPI:", "");
                    break;
                case AndroidApp app_Android:
                    app_Android.Invoke("PostTestImageToAPI");
                    break;

                default:
                    throw new System.NotSupportedException($"IApp {typeof(IApp)} is not supported");
            }
        }

        internal static void DeleteTestFoodFromAPI(IApp app)
        {
            switch (app)
            {
                case iOSApp app_iOS:
                    app_iOS.Invoke("deleteTestFoodFromAPI:", "");
                    break;
                case AndroidApp app_Android:
                    app_Android.Invoke("DeleteTestFoodFromAPI");
                    break;

                default:
                    throw new System.NotSupportedException($"IApp {typeof(IApp)} is not supported");
            }
        }
    }
}
