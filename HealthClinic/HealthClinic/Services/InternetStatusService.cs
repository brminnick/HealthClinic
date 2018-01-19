using Xamarin.Forms;

using HealthClinic.Shared;

namespace HealthClinic
{
    public class InternetStatusService : IInternetStatusService
    {
        #region Fields
        static int _networkIndicatorCount = 0;
        #endregion

        #region Methods
        public void UpdateInternetIndicatorStatus(bool isInternetConnectionActive)
        {
            if (isInternetConnectionActive)
            {
                Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.IsBusy = true);
                _networkIndicatorCount++;
            }
            else if (--_networkIndicatorCount <= 0)
            {
                Device.BeginInvokeOnMainThread(() => Application.Current.MainPage.IsBusy = false);
                _networkIndicatorCount = 0;
            }
        }
        #endregion
    }
}
