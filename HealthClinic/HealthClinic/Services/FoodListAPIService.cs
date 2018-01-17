using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HealthClinic
{
    abstract class FoodListAPIService : BaseHttpClientService
    {
        public static Task<List<FoodLogModel>> GetFoodLogs() =>
            GetDataObjectFromAPI<List<FoodLogModel>>("https://mercuryhealth-dev.azurewebsites.net/api/FoodLogApi/");

        public static Task<HttpResponseMessage> PostFoodPhoto(byte[] foodPhoto) =>
            PostObjectToAPI("https://abelmercuryhealthservice-dev.azurewebsites.net/ImageIDAPI/UploadFoodImage1", foodPhoto);
    }
}
