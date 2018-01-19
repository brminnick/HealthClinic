#if DEBUG
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using HealthClinic.Shared;

namespace HealthClinic
{
    public static class UITestBackdoorMethodServices
    {
        #region Constant Fields
        const string _testFoodDescription = "PIZZA";
        #endregion

        #region Methods
        public static Task PostTestImageToAPI() => FoodListAPIService.PostFoodPhoto(GetTestImage());

        public static async Task DeleteTestFoodFromAPI()
        {
            var allFoodItems = await FoodListAPIService.GetFoodLogs().ConfigureAwait(false);
            var pizzaFoodItemList = allFoodItems?.Where(x => x.Description.ToUpper().Equals(_testFoodDescription.ToUpper())).ToList() ?? new List<FoodLogModel>();

            var deletePizzaTaskList = new List<Task>();
            foreach (var pizzaFoodItem in pizzaFoodItemList)
                deletePizzaTaskList.Add(FoodListAPIService.DeleteFoodFromAPI(pizzaFoodItem.Id));

            await Task.WhenAll(deletePizzaTaskList);
        }

        static byte[] GetTestImage()
        {
            var file = File.OpenRead("pizza.png");
            return StreamExtensions.ConvertStreamToByteArrary(file);
        }
        #endregion
    }
}
#endif
