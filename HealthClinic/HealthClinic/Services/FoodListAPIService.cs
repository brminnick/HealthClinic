using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HealthClinic
{
    abstract class FoodListAPIService : BaseHttpClientService
    {
        public static Task<List<FoodLogModel>> GetFoodLogs()
        {
            AppCenterService.TrackEvent(AppCenterConstants.GetFoodLogsFromAPITriggered);
            return GetDataObjectFromAPI<List<FoodLogModel>>("https://mercuryhealth-dev.azurewebsites.net/api/FoodLogApi/");
        }

        public static Task<HttpResponseMessage> PostFoodPhoto(byte[] foodPhoto)
        {
            AppCenterService.TrackEvent(AppCenterConstants.UploadPhotoToAPITriggered);
            return PostObjectToAPI("https://abelmercuryhealthservice-dev.azurewebsites.net/ImageIDAPI/UploadFoodImage1", foodPhoto);
        }
    }
}
