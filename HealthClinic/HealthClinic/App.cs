using System;

using Xamarin.Forms;

using Microsoft.AppCenter;

using HealthClinic.Shared;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace HealthClinic
{
    public class App : Application
    {
        public App() => MainPage = new HealthClinicNavigationPage(new FoodListPage());

        protected override void OnStart()
        {
            base.OnStart();

            BaseHttpClientService.InternetStatusService = new InternetStatusService();

            switch (Xamarin.Forms.Device.RuntimePlatform)
            {
                case Xamarin.Forms.Device.Android:
                    AppCenter.Start(AppCenterConstants.AppCenterAPIKey_Droid, typeof(Analytics), typeof(Crashes));
                    break;

                case Xamarin.Forms.Device.iOS:
                    AppCenter.Start(AppCenterConstants.AppCenterAPIKey_iOS, typeof(Analytics), typeof(Crashes));
                    break;

                default:
                    throw new NotSupportedException("Runtime Not Supported");
            }
        }
    }
}
