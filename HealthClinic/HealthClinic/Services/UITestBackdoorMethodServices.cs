#if DEBUG
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using HealthClinic.Shared;

namespace HealthClinic
{
    public static class UITestBackdoorMethodServices
    {
        #region Constant Fields
        const string _testFoodDescription = "PIZZA";
        #endregion

        #region Fields
        static AddFoodViewModel _addFoodViewModel;
        static Stream _testImageAsStream;
        static byte[] _testImageAsByteArray;
        #endregion

        #region Properties
        static AddFoodViewModel AddFoodViewModel
        {
            get
            {
                if (_addFoodViewModel == null)
                {
                    var mainPageNavigationPage = Application.Current.MainPage as HealthClinicNavigationPage;
                    var addFoodPageNavigationPage = mainPageNavigationPage.Navigation.ModalStack.FirstOrDefault() as HealthClinicNavigationPage;

                    var addFoodPage = addFoodPageNavigationPage?.CurrentPage as AddFoodPage;

                    _addFoodViewModel = addFoodPage?.BindingContext as AddFoodViewModel;
                }

                return _addFoodViewModel;
            }
        }

        static Stream TestImageAsStream
        {
            get
            {
                if (_testImageAsStream == null)
                    _testImageAsStream = File.OpenRead("pizza.png");

                return _testImageAsStream;
            }
        }

        static byte[] TestImageAsByteArray
        {
            get
            {
                if (_testImageAsByteArray == null)
                    _testImageAsByteArray = StreamExtensions.ConvertStreamToByteArrary(TestImageAsStream);

                return _testImageAsByteArray;
            }
        }
        #endregion

        #region Methods
        public static Task PostTestImageToAPI() => FoodListAPIService.PostFoodPhoto(TestImageAsByteArray);

        public static async Task DeleteTestFoodFromAPI()
        {
            var allFoodItems = await FoodListAPIService.GetFoodLogs().ConfigureAwait(false);
            var pizzaFoodItemList = allFoodItems?.Where(x => x.Description.ToUpper().Equals(_testFoodDescription.ToUpper())).ToList() ?? new List<FoodLogModel>();

            var deletePizzaTaskList = new List<Task>();
            foreach (var pizzaFoodItem in pizzaFoodItemList)
                deletePizzaTaskList.Add(FoodListAPIService.DeleteFoodFromAPI(pizzaFoodItem.Id));

            await Task.WhenAll(deletePizzaTaskList);
        }

        public static void InjectImageIntoAddFoodPage()
        {
            AddFoodViewModel.PhotoBlob = TestImageAsByteArray;
            AddFoodViewModel.PhotoImageSource = ImageSource.FromStream(() => TestImageAsStream);
        }
        #endregion
    }
}
#endif
