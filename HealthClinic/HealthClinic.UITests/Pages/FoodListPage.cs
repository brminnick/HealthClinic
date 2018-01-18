using Xamarin.UITest;

using HealthClinic.Shared;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace HealthClinic.UITests
{
    public class FoodListPage : BasePage
    {
        readonly Query _addFoodButton, _foodListView;

        public FoodListPage(IApp app) : base(app, PageTitleConstants.FoodListPage)
        {
            _addFoodButton = x => x.Marked(AutomationIdConstants.FoodListPage_AddFoodButton);
            _foodListView = x => x.Marked(AutomationIdConstants.FoodListPage_FoodList);
        }

        public void TapAddFoodButton()
        {
            App.Tap(_addFoodButton);
            App.Screenshot("Add Food Button Tapped");
        }
    }
}
