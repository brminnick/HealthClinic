using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace HealthClinic
{
    public class FoodListViewModel : BaseViewModel
    {
        #region Fields
        bool _isRefreshing;
        List<FoodLogModel> _foodList;
        ICommand _pullToRefreshCommand;
        #endregion

        #region Properties
        public ICommand PullToRefreshCommand => _pullToRefreshCommand ??
            (_pullToRefreshCommand = new Command(async () => await ExecutePullToRefreshCommand()));

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public List<FoodLogModel> FoodList
        {
            get => _foodList;
            set => SetProperty(ref _foodList, value);
        }
        #endregion

        #region Methods
        async Task ExecutePullToRefreshCommand()
        {
            IsRefreshing = true;

            try
            {
                var unsortedFoodList = await FoodListAPIService.GetFoodLogs();
                FoodList = unsortedFoodList.OrderBy(x => x.MealTime).ToList();
            }
            finally
            {
                IsRefreshing = false;
            }
        }
        #endregion
    }
}
