using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using HealthClinic.Shared;

namespace HealthClinic
{
    public abstract class FoodListAPIService : BaseHttpClientService
    {
        public static Task<IReadOnlyList<FoodLogModel>> GetFoodLogs()
        {
            AppCenterService.TrackEvent(AppCenterConstants.GetFoodLogsFromAPITriggered);
            return GetObjectFromAPI<IReadOnlyList<FoodLogModel>>(APIConstants.GetFoodLogsUrl);
        }

        public static Task<HttpResponseMessage> PostFoodPhoto(byte[] foodPhoto)
        {
            AppCenterService.TrackEvent(AppCenterConstants.UploadPhotoToAPITriggered);
            return PostObjectToAPI(APIConstants.PostFoodUrl, foodPhoto);
        }

        public static Task<HttpResponseMessage> DeleteFoodFromAPI(int id)
        {
            AppCenterService.TrackEvent(AppCenterConstants.DeleteFoodAPITriggered);
            return GetObjectFromAPI($"{APIConstants.DeleteFoodLogUrl}?id={id}");
        }
    }
}
