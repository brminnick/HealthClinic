using System;
using System.IO;
using System.Windows.Input;
using System.Threading.Tasks;

using Xamarin.Forms;

using HealthClinic.Shared;
using AsyncAwaitBestPractices;
using AsyncAwaitBestPractices.MVVM;

namespace HealthClinic
{
    public class AddFoodViewModel : BaseViewModel
    {
        #region Constant Fields
        readonly WeakEventManager _uploadPhotoCompletedEventManager = new WeakEventManager();
        readonly WeakEventManager<string> _uploadPhotoFailedEventManager = new WeakEventManager<string>();
        #endregion

        #region Fields
        ICommand _takePhotoCommand, _uploadButtonCommand;
        ImageSource _photoImageSource;
        bool _isPhotoUploading;
        #endregion

        #region Events
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
        #endregion

        #region Properties
        public ICommand TakePhotoCommand => _takePhotoCommand ??
            (_takePhotoCommand = new AsyncCommand(ExecuteTakePhotoCommand, continueOnCapturedContext: false));

        public ICommand UploadButtonCommand => _uploadButtonCommand ??
            (_uploadButtonCommand = new AsyncCommand(ExecuteUploadButtonCommand, continueOnCapturedContext: false));

        public bool IsPhotoUploading
        {
            get => _isPhotoUploading;
            set => SetProperty(ref _isPhotoUploading, value);
        }

        public ImageSource PhotoImageSource
        {
            get => _photoImageSource;
            set => SetProperty(ref _photoImageSource, value);
        }

        public byte[] PhotoBlob { get; set; }
        #endregion

        #region Methods
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

                if (postPhotoBlobResponse is null || postPhotoBlobResponse?.IsSuccessStatusCode is false)
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

        void OnUploadPhotoFailed(string errorMessage) => _uploadPhotoFailedEventManager.HandleEvent(this, errorMessage, nameof(UploadPhotoFailed));
        void OnSavePhotoCompleted() => _uploadPhotoCompletedEventManager.HandleEvent(this, EventArgs.Empty, nameof(UploadPhotoCompleted));
        #endregion
    }
}
