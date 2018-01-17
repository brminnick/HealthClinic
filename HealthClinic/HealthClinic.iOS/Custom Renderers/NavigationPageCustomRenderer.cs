using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using HealthClinic.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(NavigationPageCustomRenderer))]
namespace HealthClinic.iOS
{
    public class NavigationPageCustomRenderer : NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                NavigationBar.PrefersLargeTitles = true;

                NavigationBar.LargeTitleTextAttributes = new UIStringAttributes
                {
                    ForegroundColor = ColorConstants.OffWhite.ToUIColor()
                };
            }
        }
    }
}
