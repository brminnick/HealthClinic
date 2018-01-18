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

        [Test]
        public void LaunchTest()
        {
            
        }

        [Test]
        public void AddFoodPageTest()
        {
            //Arrange

            //Act
            FoodListPage.TapAddFoodButton();

            //Assert
            AddFoodPage.WaitForPageToLoad();
        }
    }
}
