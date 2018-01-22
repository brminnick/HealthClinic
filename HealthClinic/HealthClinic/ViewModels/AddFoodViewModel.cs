using System;
using System.IO;
using System.Windows.Input;
using System.Threading.Tasks;

using Xamarin.Forms;

using HealthClinic.Shared;

namespace HealthClinic
{
    public class AddFoodViewModel : BaseViewModel
    {
        #region Fields
        ICommand _takePhotoCommand, _uploadButtonCommand;
        ImageSource _photoImageSource;
        bool _isPhotoUploading;
        #endregion

        #region Events
        public event EventHandler UploadPhotoCompleted;
        public event EventHandler<string> UploadPhotoFailed;
        #endregion

        #region Properties
        public ICommand TakePhotoCommand => _takePhotoCommand ??
            (_takePhotoCommand = new Command(async () => await ExecuteTakePhotoCommand()));

        public ICommand UploadButtonCommand => _uploadButtonCommand ??
            (_uploadButtonCommand = new Command(async () => await ExecuteUploadButtonCommand()));

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

            if (mediaFile == null)
                return;

            var photoBlobStream = mediaFile.GetStream();
            PhotoBlob = StreamExtensions.ConvertStreamToByteArrary(photoBlobStream);

            PhotoImageSource = ImageSource.FromStream(() => new MemoryStream(PhotoBlob));
        }

        async Task ExecuteUploadButtonCommand()
        {
            if (IsPhotoUploading)
                return;

            if (PhotoBlob == null)
            {
                OnUploadPhotoFailed("Take Photo First");
                return;
            }

            IsPhotoUploading = true;

            try
            {
                var postPhotoBlobResponse = await FoodListAPIService.PostFoodPhoto(PhotoBlob).ConfigureAwait(false);

                if (postPhotoBlobResponse?.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                {
                    OnUploadPhotoFailed("No Food Found");
                    return;
                }

                if (postPhotoBlobResponse == null || postPhotoBlobResponse?.IsSuccessStatusCode == false)
                {
                    OnUploadPhotoFailed($"Status Code: {postPhotoBlobResponse?.ReasonPhrase ?? "null"}");
                    return;
                }

                OnSavePhotoCompleted();
            }
            catch (Exception e)
            {
                AppCenterService.LogException(e);
                OnUploadPhotoFailed(e.Message);
            }
            finally
            {
                IsPhotoUploading = false;
            }
        }

        void OnUploadPhotoFailed(string errorMessage) => UploadPhotoFailed?.Invoke(this, errorMessage);
        void OnSavePhotoCompleted() => UploadPhotoCompleted?.Invoke(this, EventArgs.Empty);
        #endregion
    }
}
