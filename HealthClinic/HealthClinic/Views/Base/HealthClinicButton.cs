using System;

using Xamarin.Forms;

using HealthClinic.Shared;

namespace HealthClinic
{
    public class HealthClinicButton : Button
    {
        public HealthClinicButton() => TextColor = Color.FromHex(ColorConstants.TextHex);
    }
}
