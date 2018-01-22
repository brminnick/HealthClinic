using Xamarin.UITest;

using HealthClinic.Shared;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;

namespace HealthClinic.UITests
{
    public class FoodListPage : BasePage
    {
        #region Constant Fields
        readonly Query _addFoodButton, _foodListView;
        #endregion

        #region Constructors
        public FoodListPage(IApp app) : base(app, PageTitleConstants.FoodListPage)
        {
            _addFoodButton = x => x.Marked(AutomationIdConstants.FoodListPage_AddFoodButton);
            _foodListView = x => x.Marked(AutomationIdConstants.FoodListPage_FoodList);
        }
        #endregion

        #region Methods
        public void TapAddFoodButton()
        {
            App.Tap(_addFoodButton);
            App.Screenshot("Add Food Button Tapped");
        }

        public bool DoesFoodExistInList(string foodDescription)
        {
            try
            {
                App.ScrollDownTo(foodDescription);
                return true;
            }
            catch
            {
                return false;
            }
        }
		#endregion
    }
}
