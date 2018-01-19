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

        protected override void SubscribeEventHandlers()
        {
            _addFoodButton.Clicked += HandleAddFoodButtonClicked;
            _foodListView.ItemSelected += HandleItemSelected;

        }

        protected override void UnsubscribeEventHandlers()
        {
            _addFoodButton.Clicked -= HandleAddFoodButtonClicked;
            _foodListView.ItemSelected -= HandleItemSelected;
        }

        void HandleAddFoodButtonClicked(object sender, EventArgs e)
        {
            AppCenterService.TrackEvent(AppCenterConstants.AddFoodListPageButtonTapped);
            Device.BeginInvokeOnMainThread(async () => await Navigation.PushModalAsync(new HealthClinicNavigationPage(new AddFoodPage())));
        }

        void HandleItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var itemTapped = e.SelectedItem as FoodLogModel;

            AppCenterService.TrackEvent(AppCenterConstants.FoodListItemTapped,
                                        new Dictionary<string, string> { { AppCenterConstants.Description, itemTapped?.Description_PascalCase } });
            
            _foodListView.SelectedItem = null;
        }
        #endregion
    }
}
