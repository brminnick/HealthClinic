using System;
using System.Threading.Tasks;

using AsyncAwaitBestPractices;

using Plugin.Media;
using Plugin.Media.Abstractions;

using Xamarin.Forms;

namespace HealthClinic
{
    public static class MediaService
    {
        #region Constant Fields
        readonly static WeakEventManager _noCameraFoundEventManager = new WeakEventManager();
        #endregion

        #region Events
        public static event EventHandler NoCameraFound
        {
            add => _noCameraFoundEventManager.AddEventHandler(value);
            remove => _noCameraFoundEventManager.RemoveEventHandler(value);
        }
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

        static void OnNoCameraFound() => _noCameraFoundEventManager.HandleEvent(null, EventArgs.Empty, nameof(NoCameraFound));
        #endregion
    }
}
