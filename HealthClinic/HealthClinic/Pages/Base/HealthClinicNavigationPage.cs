using Xamarin.Forms;

namespace HealthClinic
{
    public class HealthClinicNavigationPage : NavigationPage
    {
        public HealthClinicNavigationPage(Page root) : base(root)
        {
            BarTextColor = ColorConstants.OffWhite;
            BarBackgroundColor = ColorConstants.Maroon;
        }
    }
}
