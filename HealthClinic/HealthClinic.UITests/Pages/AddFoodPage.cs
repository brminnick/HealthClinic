using Xamarin.UITest;

using HealthClinic.Shared;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace HealthClinic.UITests
{
    public class AddFoodPage : BasePage
    {
        #region Constant Fields
        readonly Query _uploadButton, _cancelButton, _takePhotoButton, _activityIndicator;
        #endregion

        #region Constructors
        public AddFoodPage(IApp app) : base(app, PageTitleConstants.AddFoodPage)
        {
            _uploadButton = x => x.Marked(AutomationIdConstants.AddFoodPage_UploadButton);
            _cancelButton = x => x.Marked(AutomationIdConstants.AddFoodPage_CancelButton);
            _takePhotoButton = x => x.Marked(AutomationIdConstants.AddFoodPage_TakePhotoButton);
            _activityIndicator = x => x.Marked(AutomationIdConstants.AddFoodPage_ActivityIndicator);
        }
        #endregion

        #region Methods
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

        public void TapOkDialog()
        {
            App.Tap("OK");
            App.Screenshot("Tapped Ok");
        }

        public void WaitForActivityIndicator()
        {
            App.WaitForElement(_activityIndicator);
            App.Screenshot("Waited For Activity Indicator");
        }

        public void WaitForNoActivityIndicator()
        {
            App.WaitForNoElement(_activityIndicator, timeout: HttpConstants.HttpTimeOut);
            App.Screenshot("Waited For No Activity Indicator");
        }
        #endregion
    }
}
