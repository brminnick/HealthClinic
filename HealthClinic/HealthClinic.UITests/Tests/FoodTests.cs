using NUnit.Framework;

using Xamarin.UITest;

namespace HealthClinic.UITests
{
    public class FoodTests : BaseTest
    {
        public FoodTests(Platform platform) : base(platform)
        {
        }

        public override void TestSetup()
        {
            base.TestSetup();

            FoodListPage.WaitForPageToLoad();
        }

        public override void TestTearDown()
        {
            base.TestTearDown();

            BackdoorMethodServices.DeleteTestFoodFromAPI(App);
        }


        [Test]
        public void UploadFoodTest()
        {
            //Arrange
            const string testFoodDescription = "Pizza";

            //Act
            FoodListPage.TapAddFoodButton();

            BackdoorMethodServices.InjectImageIntoAddFoodPage(App);
            AddFoodPage.TapUploadButton();

            AddFoodPage.WaitForActivityIndicator();
            AddFoodPage.WaitForNoActivityIndicator();

            AddFoodPage.TapOkDialog();

            //Assert
            FoodListPage.WaitForPageToLoad();
            FoodListPage.DoesFoodExistInList(testFoodDescription);
        }
    }
}
