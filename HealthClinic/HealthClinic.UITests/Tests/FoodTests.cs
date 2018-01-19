using System.Reflection;
using System.Threading.Tasks;

using NUnit.Framework;

using Xamarin.UITest;

using HealthClinic.Shared;

namespace HealthClinic.UITests
{
    public class FoodTests : BaseTest
    {
        const string _testFoodDescription = "pizza";

        public FoodTests(Platform platform) : base(platform)
        {
        }

        public override void TestSetup()
        {
            var addTestFoodTask = AddTestFood();

            base.TestSetup();

            FoodListPage.WaitForPageToLoad();
        }

        public override void TestTearDown()
        {
            base.TestTearDown();
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

        async Task AddTestFood()
        {
            await FoodListAPIService.PostFoodPhoto(GetTestImage());
        }

        byte[] GetTestImage() =>
            Assembly.GetExecutingAssembly().GetManifestResourceStream("pizza.png").ConvertToByteArray();
    }
}
