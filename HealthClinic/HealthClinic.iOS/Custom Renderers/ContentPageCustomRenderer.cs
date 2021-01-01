using System.Collections.Generic;
using System.Linq;
using HealthClinic.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageCustomRenderer))]
namespace HealthClinic.iOS
{
    public class ContentPageCustomRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var contentPage = (ContentPage)Element;

            var leftNavList = new List<UIBarButtonItem>();
            var rightNavList = new List<UIBarButtonItem>();

            var navigationItem = NavigationController.TopViewController.NavigationItem;

            if (navigationItem?.LeftBarButtonItems?.Any() is true)
                return;

            for (var i = 0; i < contentPage.ToolbarItems.Count; i++)
            {
                var reorder = contentPage.ToolbarItems.Count - 1;
                var itemPriority = contentPage.ToolbarItems[reorder - i].Priority;

                if (itemPriority is 1)
                {
                    var leftNavItems = navigationItem?.RightBarButtonItems?[i];

                    if (leftNavItems is not null)
                        leftNavList.Add(leftNavItems);
                }
                else if (itemPriority is 0)
                {
                    var rightNavItems = navigationItem?.RightBarButtonItems?[i];

                    if (rightNavItems is not null)
                        rightNavList.Add(rightNavItems);
                }
            }

            navigationItem?.SetLeftBarButtonItems(leftNavList.ToArray(), false);
            navigationItem?.SetRightBarButtonItems(rightNavList.ToArray(), false);
        }
    }
}