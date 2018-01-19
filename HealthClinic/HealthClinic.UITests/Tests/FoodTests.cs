using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

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

            BackdoorMethodServices.PostTestImageToAPI(App);
        }

        public override void TestTearDown()
        {
            base.TestTearDown();

			BackdoorMethodServices.DeleteTestFoodFromAPI(App);
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
