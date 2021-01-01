using System;
using System.Threading.Tasks;
using HealthClinic.Shared;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HealthClinic
{
    public class AddFoodPage : BaseContentPage<AddFoodViewModel>
    {
        public AddFoodPage() : base(PageTitleConstants.AddFoodPage)
        {
            MediaService.NoCameraFound += HandleNoCameraFound;
            ViewModel.UploadPhotoFailed += HandleUploadPhotoFailed;
            ViewModel.UploadPhotoCompleted += HandleUploadPhotoCompleted;

            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Upload",
                Priority = 0,
                AutomationId = AutomationIdConstants.AddFoodPage_UploadButton,
            }.Bind(MenuItem.CommandProperty, nameof(ViewModel.UploadButtonCommand)));

            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Cancel",
                Priority = 1,
                AutomationId = AutomationIdConstants.AddFoodPage_CancelButton
            }.Bind<ToolbarItem, bool, bool>(ToolbarItem.IsEnabledProperty, nameof(AddFoodViewModel.IsPhotoUploading), convert: isPhotoUploading => !isPhotoUploading)
             .Invoke(cancelButton => cancelButton.Clicked += HandleCancelToolbarItemClicked));

            Padding = new Thickness(20);

            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Spacing = 20,

                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.FillAndExpand,

                    Children =
                    {
                        new Image()
                            .Bind(Image.SourceProperty, nameof(ViewModel.PhotoImageSource)),

                        new HealthClinicButton { Text = "Take Photo" }
                            .Bind(Button.CommandProperty, nameof(ViewModel.TakePhotoCommand))
                            .Bind<Button, bool, bool>(IsEnabledProperty, nameof(ViewModel.IsPhotoUploading), convert: isPhotoUploading => !isPhotoUploading),

                        new ActivityIndicator { Color = ColorConstants.Maroon, AutomationId = AutomationIdConstants.AddFoodPage_ActivityIndicator }
                            .Bind(IsVisibleProperty, nameof(ViewModel.IsPhotoUploading))
                            .Bind(ActivityIndicator.IsRunningProperty, nameof(ViewModel.IsPhotoUploading))
                    }
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            AppCenterService.TrackEvent(AppCenterConstants.AddFoodListPageAppeared);
        }

        void HandleCancelToolbarItemClicked(object sender, EventArgs e)
        {
            AppCenterService.TrackEvent(AppCenterConstants.CancelButtonTapped);
            ClosePage();
        }

        void HandleUploadPhotoCompleted(object sender, EventArgs e)
        {
            AppCenterService.TrackEvent(AppCenterConstants.UploadPhotoToSucceeded);

            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Photo Saved", string.Empty, "OK");
                await ClosePage();
            });
        }

        async void HandleUploadPhotoFailed(object sender, string errorMessage)
        {
            AppCenterService.TrackEvent(AppCenterConstants.UploadPhotoFailed, AppCenterConstants.Error, errorMessage);

            await DisplayErrorMessage(errorMessage);
        }

        async void HandleNoCameraFound(object sender, EventArgs e)
        {
            AppCenterService.TrackEvent(AppCenterConstants.NoCameraFound);

            await DisplayErrorMessage("No Camera Found");
        }

        Task DisplayErrorMessage(string message) =>
            MainThread.InvokeOnMainThreadAsync(() => DisplayAlert("Error", message, "OK"));

        Task ClosePage() => MainThread.InvokeOnMainThreadAsync(() => Navigation.PopModalAsync());
    }
}
