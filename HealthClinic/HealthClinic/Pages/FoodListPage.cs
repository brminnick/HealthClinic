using System;
using System.Collections.Generic;

using Xamarin.Forms;

using HealthClinic.Shared;

namespace HealthClinic
{
    public class FoodListPage : BaseContentPage<FoodListViewModel>
    {
        #region Constant Fields
        readonly ListView _foodListView;
        readonly ToolbarItem _addFoodButton;
        #endregion

        #region Constructors
        public FoodListPage() : base(PageTitleConstants.FoodListPage)
        {
            _addFoodButton = new ToolbarItem
            {
                Text = "+",
                AutomationId = AutomationIdConstants.FoodListPage_AddFoodButton
            };
            _addFoodButton.Clicked += HandleAddFoodButtonClicked;

            ToolbarItems.Add(_addFoodButton);

            _foodListView = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                ItemTemplate = new DataTemplate(typeof(FoodViewCell)),
                IsPullToRefreshEnabled = true,
                AutomationId = AutomationIdConstants.FoodListPage_FoodList,
                BackgroundColor = ColorConstants.OffWhite,
                SeparatorVisibility = SeparatorVisibility.None,
                RowHeight = 230
            };
            _foodListView.ItemTapped += HandleItemTapped;
            _foodListView.SetBinding(ListView.ItemsSourceProperty, nameof(ViewModel.FoodList));
            _foodListView.SetBinding(ListView.IsRefreshingProperty, nameof(ViewModel.IsRefreshing));
            _foodListView.SetBinding(ListView.RefreshCommandProperty, nameof(ViewModel.PullToRefreshCommand));

            Content = _foodListView;
        }
        #endregion

        #region Methods
        protected override void OnAppearing()
        {
            base.OnAppearing();

            AppCenterService.TrackEvent(AppCenterConstants.FoodListPageAppeared);

            Device.BeginInvokeOnMainThread(_foodListView.BeginRefresh);
        }

        void HandleAddFoodButtonClicked(object sender, EventArgs e)
        {
            AppCenterService.TrackEvent(AppCenterConstants.AddFoodListPageButtonTapped);
            Device.BeginInvokeOnMainThread(async () => await Navigation.PushModalAsync(new HealthClinicNavigationPage(new AddFoodPage())));
        }

        void HandleItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item is FoodLogModel itemTapped)
            {
                AppCenterService.TrackEvent(AppCenterConstants.FoodListItemTapped,
                                            new Dictionary<string, string> { { AppCenterConstants.Description, itemTapped?.Description_PascalCase } });

                _foodListView.SelectedItem = null;
            }
        }
        #endregion
    }
}
