#if DEBUG
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xamarin.Forms;

using HealthClinic.Shared;

namespace HealthClinic
{
    public static class UITestBackdoorMethodServices
    {
        const string _testFoodDescription = "pizza";

        static AddFoodViewModel? _addFoodViewModel;
        static Stream? _testImageAsStream;
        static byte[]? _testImageAsByteArray;

        static AddFoodViewModel AddFoodViewModel
        {
            get
            {
                if (_addFoodViewModel is null)
                {
                    var mainPageNavigationPage = (HealthClinicNavigationPage)Application.Current.MainPage;
                    var addFoodPageNavigationPage = (NavigationPage)mainPageNavigationPage.Navigation.ModalStack.First();

                    var addFoodPage = (AddFoodPage)addFoodPageNavigationPage.CurrentPage;

                    _addFoodViewModel = (AddFoodViewModel)addFoodPage.BindingContext;
                }

                return _addFoodViewModel;
            }
        }

        static Stream TestImageAsStream
        {
            get
            {
                if (_testImageAsStream is null)
                {
                    var applicationTypeInfo = Application.Current.GetType().GetTypeInfo();
                    _testImageAsStream = applicationTypeInfo.Assembly.GetManifestResourceStream($"{applicationTypeInfo.Namespace}.{_testFoodDescription}.png");
                }

                return _testImageAsStream;
            }
        }

        static byte[] TestImageAsByteArray
        {
            get
            {
                if (_testImageAsByteArray is null)
                    _testImageAsByteArray = StreamExtensions.ConvertStreamToByteArrary(TestImageAsStream);

                return _testImageAsByteArray;
            }
        }

        public static Task PostTestImageToAPI() => FoodListAPIService.PostFoodPhoto(TestImageAsByteArray);

        public static async Task DeleteTestFoodFromAPI()
        {
            var allFoodItems = await FoodListAPIService.GetFoodLogs().ConfigureAwait(false);
            var pizzaFoodItemList = allFoodItems?.Where(x => x.Description.ToUpper().Equals(_testFoodDescription.ToUpper())).ToList() ?? new List<FoodLogModel>();

            var deletePizzaTaskList = new List<Task>();
            foreach (var pizzaFoodItem in pizzaFoodItemList)
                deletePizzaTaskList.Add(FoodListAPIService.DeleteFoodFromAPI(pizzaFoodItem.Id));

            await Task.WhenAll(deletePizzaTaskList).ConfigureAwait(false);
        }

        public static void InjectImageIntoAddFoodPage()
        {
            AddFoodViewModel.PhotoBlob = TestImageAsByteArray;
            AddFoodViewModel.PhotoImageSource = ImageSource.FromStream(() => TestImageAsStream);
        }
    }
}
#endif
