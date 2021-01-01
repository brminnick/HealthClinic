using HealthClinic.Shared;
using NUnit.Framework;

using Xamarin.UITest;

namespace HealthClinic.UITests
{
    class FoodTests : BaseTest
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

            App.InvokeBackdoorMethod(BackdoorMethodConstants.DeleteTestFoodFromAPI);
        }


        [Test]
        public void UploadFoodTest()
        {
            //Arrange
            const string testFoodDescription = "Pizza";

            //Act
            FoodListPage.TapAddFoodButton();

            App.InvokeBackdoorMethod(BackdoorMethodConstants.InjectImageIntoAddFoodPage);
            AddFoodPage.TapUploadButton();

            try
            {
                AddFoodPage.WaitForActivityIndicator();
            }
            catch
            {

            }

            AddFoodPage.WaitForNoActivityIndicator();

            AddFoodPage.TapOkDialog();

            //Assert
            FoodListPage.WaitForPageToLoad();
            FoodListPage.DoesFoodExistInList(testFoodDescription);
        }
    }
}
