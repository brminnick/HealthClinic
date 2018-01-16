using Xamarin.Forms;

using HealthClinic.Shared;

namespace HealthClinic
{
    public class HealthClinicNavigationPage : NavigationPage
    {
        public HealthClinicNavigationPage(Page root) : base(root)
        {
            BarTextColor = Color.FromHex(ColorConstants.NavigationBarTextHex);
            BarBackgroundColor = Color.FromHex(ColorConstants.NavigationBarBackgroundHex);
        }
    }
}
