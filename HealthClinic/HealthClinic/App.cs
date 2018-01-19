using Xamarin.Forms;

using HealthClinic.Shared;

namespace HealthClinic
{
    public class App : Application
    {
        public App() => MainPage = new HealthClinicNavigationPage(new FoodListPage());

        protected override void OnStart()
        {
            base.OnStart();

            AppCenterService.Start();
        }
    }
}
