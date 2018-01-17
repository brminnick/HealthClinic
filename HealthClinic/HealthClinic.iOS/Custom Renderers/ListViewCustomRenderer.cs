using System.Linq;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using HealthClinic.iOS;

[assembly: ExportRenderer(typeof(ListView), typeof(ListViewCustomRenderer))]
namespace HealthClinic.iOS
{
    public class ListViewCustomRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            var refreshControl = Control?.Subviews?.FirstOrDefault(x => x is UIRefreshControl) as UIRefreshControl;
            if (refreshControl == null)
                return;

            refreshControl.TintColor = ColorConstants.OffWhite.ToUIColor();
        }
    }
}
