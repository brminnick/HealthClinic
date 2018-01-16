using System;

using Xamarin.Forms;

using FFImageLoading.Forms;

using HealthClinic.Shared;

namespace HealthClinic
{
    public class AddFoodPage : BaseContentPage<AddFoodViewModel>
    {
        #region Constant Fields
        readonly Button _takePhotoButton;
        readonly CachedImage _photoImage;
        readonly ToolbarItem _uploadToolbarItem, _cancelToolbarItem;
        #endregion

        #region Constructors
        public AddFoodPage() : base(PageTitleConstants.AddFoodPage)
        {
            _takePhotoButton = new Button
            {
                Text = "Take Photo",
            };
            _takePhotoButton.SetBinding(Button.CommandProperty, nameof(ViewModel.TakePhotoCommand));
            _takePhotoButton.SetBinding(IsEnabledProperty, new Binding(nameof(ViewModel.IsPhotoUploading), BindingMode.Default, new InverseBooleanConverter(), ViewModel.IsPhotoUploading));

            _photoImage = new CachedImage();
            _photoImage.SetBinding(CachedImage.SourceProperty, nameof(ViewModel.PhotoImageSource));

            _uploadToolbarItem = new ToolbarItem
            {
                Text = "Upload",
                Priority = 0,
                AutomationId = AutomationIdConstants.AddFoodPage_UploadButton,
            };
            _uploadToolbarItem.SetBinding(MenuItem.CommandProperty, nameof(ViewModel.UploadButtonCommand));
            ToolbarItems.Add(_uploadToolbarItem);

            _cancelToolbarItem = new ToolbarItem
            {
                Text = "Cancel",
                Priority = 1,
                AutomationId = AutomationIdConstants.AddFoodPage_CancelButton
            };
            ToolbarItems.Add(_cancelToolbarItem);

            var activityIndicator = new ActivityIndicator();
            activityIndicator.SetBinding(IsVisibleProperty, nameof(ViewModel.IsPhotoUploading));
            activityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, nameof(ViewModel.IsPhotoUploading));

            Padding = new Thickness(20);

            var stackLayout = new StackLayout
            {
                Spacing = 20,

                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,

                Children = {
                    _photoImage,
                    _takePhotoButton,
                    activityIndicator
                }
            };

            Content = new ScrollView { Content = stackLayout };
        }
        #endregion

        #region Methods
        protected override void SubscribeEventHandlers()
        {
            MediaService.NoCameraFound += HandleNoCameraFound;
            _cancelToolbarItem.Clicked += HandleCancelToolbarItemClicked;
            ViewModel.UploadPhotoCompleted += HandleUploadPhotoCompleted;
            ViewModel.UploadPhotoFailed += HandleUploadPhotoFailed;
        }

        protected override void UnsubscribeEventHandlers()
        {
            MediaService.NoCameraFound -= HandleNoCameraFound;
            _cancelToolbarItem.Clicked -= HandleCancelToolbarItemClicked;
            ViewModel.UploadPhotoCompleted -= HandleUploadPhotoCompleted;
            ViewModel.UploadPhotoFailed -= HandleUploadPhotoFailed;
        }

        void HandleCancelToolbarItemClicked(object sender, EventArgs e)
        {
            if (!ViewModel.IsPhotoUploading)
                ClosePage();
        }

        void HandleUploadPhotoCompleted(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Photo Saved", string.Empty, "OK");
                ClosePage();
            });
        }

        void HandleUploadPhotoFailed(object sender, string errorMessage) => DisplayErrorMessage(errorMessage);

        void HandleNoCameraFound(object sender, EventArgs e) => DisplayErrorMessage("No Camera Found");

        void DisplayErrorMessage(string message) =>
            Device.BeginInvokeOnMainThread(async () => await DisplayAlert("Error", message, "Ok"));

        void ClosePage() => Device.BeginInvokeOnMainThread(async () => await Navigation.PopModalAsync());
        #endregion
    }
}
