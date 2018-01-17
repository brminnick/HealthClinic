using Xamarin.Forms;

using HealthClinic.Shared;

namespace HealthClinic
{
    public class FoodViewCell : TextCell
    {
        public FoodViewCell()
        {
            this.SetBinding(TextProperty, nameof(FoodLogModel.Description_PascalCase));
            this.SetBinding(DetailProperty, nameof(FoodLogModel.Calories));

            DetailColor = Color.FromHex(ColorConstants.TextHex);
            TextColor = Color.FromHex(ColorConstants.TextHex);
        }
    }
}
