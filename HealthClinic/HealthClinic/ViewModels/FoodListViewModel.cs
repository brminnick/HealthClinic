using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using AsyncAwaitBestPractices.MVVM;
using HealthClinic.Shared;

namespace HealthClinic
{
    public class FoodListViewModel : BaseViewModel
    {
        bool _isRefreshing;
        ICommand? _pullToRefreshCommand;
        IReadOnlyList<FoodLogModel> _foodList = Array.Empty<FoodLogModel>();

        public ICommand PullToRefreshCommand => _pullToRefreshCommand ??= new AsyncCommand(ExecutePullToRefreshCommand);

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public IReadOnlyList<FoodLogModel> FoodList
        {
            get => _foodList;
            set => SetProperty(ref _foodList, value);
        }

        async Task ExecutePullToRefreshCommand()
        {
            IsRefreshing = true;

            try
            {
                var unsortedFoodList = await FoodListAPIService.GetFoodLogs().ConfigureAwait(false);
                FoodList = unsortedFoodList.OrderBy(x => x.Description).ToList();
            }
            finally
            {
                IsRefreshing = false;
            }
        }
    }
}
