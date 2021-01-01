using System;
using System.Threading.Tasks;

using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HealthClinic
{
    public static class MediaService
    {
        readonly static AsyncAwaitBestPractices.WeakEventManager _noCameraFoundEventManager = new();

        public static event EventHandler NoCameraFound
        {
            add => _noCameraFoundEventManager.AddEventHandler(value);
            remove => _noCameraFoundEventManager.RemoveEventHandler(value);
        }

        public static async Task<MediaFile?> GetMediaFileFromCamera()
        {
            await CrossMedia.Current.Initialize().ConfigureAwait(false);

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                OnNoCameraFound();
                return null;
            }

            return await MainThread.InvokeOnMainThreadAsync(() => CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                PhotoSize = PhotoSize.Small,
                DefaultCamera = CameraDevice.Rear
            })).ConfigureAwait(false);
        }

        static void OnNoCameraFound() => _noCameraFoundEventManager.RaiseEvent(null, EventArgs.Empty, nameof(NoCameraFound));
    }
}
