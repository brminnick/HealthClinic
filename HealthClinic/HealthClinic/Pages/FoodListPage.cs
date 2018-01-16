using System;
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

        public FoodListPage() : base(PageTitleConstants.FoodListPage)
        {
            _addFoodButton = new ToolbarItem
            {
                Text = "+",
                AutomationId = AutomationIdConstants.FoodListPage_AddFoodButton
            };
            ToolbarItems.Add(_addFoodButton);

            _foodListView = new ListView
            {
                ItemTemplate = new DataTemplate(typeof(FoodViewCell)),
                IsPullToRefreshEnabled = true,
                AutomationId = AutomationIdConstants.FoodListPage_FoodList
            };
            _foodListView.SetBinding(ListView.ItemsSourceProperty, nameof(ViewModel.FoodList));
            _foodListView.SetBinding(ListView.IsRefreshingProperty, nameof(ViewModel.IsRefreshing));
            _foodListView.SetBinding(ListView.RefreshCommandProperty, nameof(ViewModel.PullToRefreshCommand));

            Content = _foodListView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Device.BeginInvokeOnMainThread(_foodListView.BeginRefresh);
        }

        protected override void SubscribeEventHandlers()
        {
            _addFoodButton.Clicked += HandleAddFoodButtonClicked;
        }

        protected override void UnsubscribeEventHandlers()
        {
            _addFoodButton.Clicked -= HandleAddFoodButtonClicked;
        }

        void HandleAddFoodButtonClicked(object sender, EventArgs e) =>
            Device.BeginInvokeOnMainThread(async () => await Navigation.PushModalAsync(new HealthClinicNavigationPage(new AddFoodPage())));
    }
}
