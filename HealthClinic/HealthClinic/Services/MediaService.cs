using System;
using System.Threading.Tasks;

using Plugin.Media;
using Plugin.Media.Abstractions;

using Xamarin.Forms;

namespace HealthClinic
{
    public static class MediaService
    {
        #region Events
        public static event EventHandler NoCameraFound;
        #endregion

        #region Methods
        public static async Task<MediaFile> GetMediaFileFromCamera()
        {
            await CrossMedia.Current.Initialize().ConfigureAwait(false);

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                OnNoCameraFound();
                return null;
            }

            var mediaFileTCS = new TaskCompletionSource<MediaFile>();
            Device.BeginInvokeOnMainThread(async () =>
            {
                mediaFileTCS.SetResult(await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Small,
                    DefaultCamera = CameraDevice.Rear
                }));
            });

            return await mediaFileTCS.Task;
        }

        static void OnNoCameraFound() => NoCameraFound?.Invoke(null, EventArgs.Empty);
        #endregion
    }
}
