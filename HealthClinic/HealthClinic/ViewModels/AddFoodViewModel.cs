using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;
using HealthClinic.Shared;
using Xamarin.Forms;

namespace HealthClinic
{
    public class AddFoodViewModel : BaseViewModel
    {
        readonly AsyncAwaitBestPractices.WeakEventManager _uploadPhotoCompletedEventManager = new();
        readonly WeakEventManager<string> _uploadPhotoFailedEventManager = new();

        ICommand? _takePhotoCommand, _uploadButtonCommand;
        ImageSource? _photoImageSource;
        bool _isPhotoUploading;

        public event EventHandler UploadPhotoCompleted
        {
            add => _uploadPhotoCompletedEventManager.AddEventHandler(value);
            remove => _uploadPhotoCompletedEventManager.RemoveEventHandler(value);
        }

        public event EventHandler<string> UploadPhotoFailed
        {
            add => _uploadPhotoFailedEventManager.AddEventHandler(value);
            remove => _uploadPhotoFailedEventManager.RemoveEventHandler(value);
        }

        public ICommand TakePhotoCommand => _takePhotoCommand ??= new AsyncCommand(ExecuteTakePhotoCommand);
        public ICommand UploadButtonCommand => _uploadButtonCommand ??= new AsyncCommand(ExecuteUploadButtonCommand);

        public bool IsPhotoUploading
        {
            get => _isPhotoUploading;
            set => SetProperty(ref _isPhotoUploading, value);
        }

        public ImageSource? PhotoImageSource
        {
            get => _photoImageSource;
            set => SetProperty(ref _photoImageSource, value);
        }

        public byte[]? PhotoBlob { get; set; }

        async Task ExecuteTakePhotoCommand()
        {
            var mediaFile = await MediaService.GetMediaFileFromCamera().ConfigureAwait(false);

            if (mediaFile is null)
                return;

            var photoBlobStream = mediaFile.GetStream();
            PhotoBlob = StreamExtensions.ConvertStreamToByteArrary(photoBlobStream);

            PhotoImageSource = ImageSource.FromStream(() => new MemoryStream(PhotoBlob));
        }

        async Task ExecuteUploadButtonCommand()
        {
            if (IsPhotoUploading)
                return;

            if (PhotoBlob is null)
            {
                OnUploadPhotoFailed("Take Photo First");
                return;
            }

            IsPhotoUploading = true;

            try
            {
                var postPhotoBlobResponse = await FoodListAPIService.PostFoodPhoto(PhotoBlob).ConfigureAwait(false);

                if (postPhotoBlobResponse?.StatusCode is System.Net.HttpStatusCode.InternalServerError)
                {
                    OnUploadPhotoFailed("No Food Found");
                    return;
                }

                if (postPhotoBlobResponse is null || postPhotoBlobResponse.IsSuccessStatusCode is false)
                {
                    OnUploadPhotoFailed($"Status Code: {postPhotoBlobResponse?.ReasonPhrase ?? "null"}");
                    return;
                }

                OnSavePhotoCompleted();
            }
            catch (Exception e)
            {
                AppCenterService.Report(e);
                OnUploadPhotoFailed(e.Message);
            }
            finally
            {
                IsPhotoUploading = false;
            }
        }

        void OnUploadPhotoFailed(string errorMessage) => _uploadPhotoFailedEventManager.RaiseEvent(this, errorMessage, nameof(UploadPhotoFailed));
        void OnSavePhotoCompleted() => _uploadPhotoCompletedEventManager.RaiseEvent(this, EventArgs.Empty, nameof(UploadPhotoCompleted));
    }
}
