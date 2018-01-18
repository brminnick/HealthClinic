using Xamarin.UITest;

using HealthClinic.Shared;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace HealthClinic.UITests
{
    public class AddFoodPage : BasePage
    {
        readonly Query _uploadButton, _cancelButton, _takePhotoButton;

        public AddFoodPage(IApp app): base(app, PageTitleConstants.AddFoodPage)
        {
            _uploadButton = x => x.Marked(AutomationIdConstants.AddFoodPage_UploadButton);
            _cancelButton = x => x.Marked(AutomationIdConstants.AddFoodPage_CancelButton);
            _takePhotoButton = x => x.Marked(AutomationIdConstants.AddFoodPage_TakePhotoButton);
        }

        public void TapUploadButton()
        {
            App.Tap(_uploadButton);
            App.Screenshot("Upload Button Tapped");
        }

        public void TapCancelButton()
        {
            App.Tap(_cancelButton);
            App.Screenshot("Cancel Button Tapped");
        }

        public void TapTakePhotoButton()
        {
            App.Tap(_takePhotoButton);
            App.Screenshot("Take Photo Button Tapped");
        }
    }
}
