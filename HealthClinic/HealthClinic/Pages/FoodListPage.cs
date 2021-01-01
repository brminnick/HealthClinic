using System;
using HealthClinic.Shared;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace HealthClinic
{
    public class FoodListPage : BaseContentPage<FoodListViewModel>
    {
        readonly ListView _foodListView;

        public FoodListPage() : base(PageTitleConstants.FoodListPage)
        {
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "+",
                AutomationId = AutomationIdConstants.FoodListPage_AddFoodButton
            }.Invoke(addFoodButton => addFoodButton.Clicked += HandleAddFoodButtonClicked));

            Content = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                ItemTemplate = new DataTemplate(typeof(FoodViewCell)),
                IsPullToRefreshEnabled = true,
                AutomationId = AutomationIdConstants.FoodListPage_FoodList,
                BackgroundColor = ColorConstants.OffWhite,
                SeparatorVisibility = SeparatorVisibility.None,
                RowHeight = 230
            }.Bind(ListView.ItemsSourceProperty, nameof(ViewModel.FoodList))
             .Bind(ListView.IsRefreshingProperty, nameof(ViewModel.IsRefreshing))
             .Bind(ListView.RefreshCommandProperty, nameof(ViewModel.PullToRefreshCommand))
             .Assign(out _foodListView)
             .Invoke(listView => listView.ItemTapped += HandleItemTapped);
        }
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
            var listView = (ListView)sender;
            listView.SelectedItem = null;

            if (e.Item is FoodLogModel itemTapped)
                AppCenterService.TrackEvent(AppCenterConstants.FoodListItemTapped, AppCenterConstants.Description, itemTapped.Description_PascalCase);
        }
    }
}
