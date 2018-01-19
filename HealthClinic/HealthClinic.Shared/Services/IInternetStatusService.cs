using System;

namespace HealthClinic.Shared
{
    public interface IInternetStatusService
    {
        void UpdateInternetIndicatorStatus(bool isInternetConnectionActive);
    }
}
