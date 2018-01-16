using Xamarin.Forms;

namespace HealthClinic
{
    public class FoodViewCell : TextCell
    {
        public FoodViewCell()
        {
            this.SetBinding(TextProperty, nameof(FoodLogModel.Description_PascalCase));
            this.SetBinding(DetailProperty, nameof(FoodLogModel.Calories));
        }
    }
}
