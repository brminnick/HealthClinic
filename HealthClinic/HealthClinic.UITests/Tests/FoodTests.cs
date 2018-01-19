using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using NUnit.Framework;

using Xamarin.UITest;

using HealthClinic.Shared;

namespace HealthClinic.UITests
{
    public class FoodTests : BaseTest
    {
        const string _testFoodDescription = "PIZZA";

        public FoodTests(Platform platform) : base(platform)
        {
        }

        public override void TestSetup()
        {
            var addTestFoodTask = AddTestFoodToBackend();

            base.TestSetup();

            FoodListPage.WaitForPageToLoad();

            Task.Run(async () => await AddTestFoodToBackend()).GetAwaiter().GetResult();
        }

        public override void TestTearDown()
        {
            base.TestTearDown();

            Task.Run(async () => await DeleteTestFromFromBackend()).GetAwaiter().GetResult();
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

        async Task DeleteTestFromFromBackend()
        {
            var allFoodItems = await FoodListAPIService.GetFoodLogs().ConfigureAwait(false);
            var pizzaFoodItemList = allFoodItems?.Where(x => x.Description.ToUpper().Equals(_testFoodDescription.ToUpper())).ToList() ?? new List<FoodLogModel>();

            var deletePizzaTaskList = new List<Task>();
            foreach (var pizzaFoodItem in pizzaFoodItemList)
                deletePizzaTaskList.Add(FoodListAPIService.DeleteFood(pizzaFoodItem.Id));

            await Task.WhenAll(deletePizzaTaskList);
        }

        Task AddTestFoodToBackend() => FoodListAPIService.PostFoodPhoto(GetTestImage());

        byte[] GetTestImage()
        {
            var file = File.OpenRead("pizza.png");
            return StreamExtensions.ConvertStreamToByteArrary(file);
        }
    }
}
