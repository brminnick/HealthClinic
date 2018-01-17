using Xamarin.Forms;

namespace HealthClinic
{
    public class FoodViewCell : ViewCell
    {
        #region Constructors
        public FoodViewCell()
        {
            var foodDescriptionLabel = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Start,
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = ColorConstants.Maroon
            };
            foodDescriptionLabel.SetBinding(Label.TextProperty, nameof(FoodLogModel.Description_PascalCase));

            var quantityTitleLabel = new FoodViewCellTitleLabel { Text = "Quantity" };
            var quantityDetailsLabel = new FoodViewCellDetailsLabel();
            quantityDetailsLabel.SetBinding(Label.TextProperty, nameof(FoodLogModel.Quantity));

            var mealTimeTitleLabel = new FoodViewCellTitleLabel { Text = "Meal Time" };
            var mealTimeDetailsLabel = new FoodViewCellDetailsLabel();
            mealTimeDetailsLabel.SetBinding(Label.TextProperty, nameof(FoodLogModel.MealTime_Formatted));

            var caloriesTitleLabel = new FoodViewCellTitleLabel { Text = "Calories" };
            var caloriesDetailsLabel = new FoodViewCellDetailsLabel();
            caloriesDetailsLabel.SetBinding(Label.TextProperty, nameof(FoodLogModel.Calories));

            var proteinTitleLabel = new FoodViewCellTitleLabel { Text = "Protein" };
            var proteinDetailsLabel = new FoodViewCellDetailsLabel();
            proteinDetailsLabel.SetBinding(Label.TextProperty, nameof(FoodLogModel.Protein_Formatted));

            var fatTitleLabel = new FoodViewCellTitleLabel { Text = "Fat" };
            var fatDetailsLabel = new FoodViewCellDetailsLabel();
            fatDetailsLabel.SetBinding(Label.TextProperty, nameof(FoodLogModel.Fat_Formatted));

            var carbsTitleLabel = new FoodViewCellTitleLabel { Text = "Carbs" };
            var carbsDetailsLabel = new FoodViewCellDetailsLabel();
            carbsDetailsLabel.SetBinding(Label.TextProperty, nameof(FoodLogModel.Carbohydrates_Formatted));

            var grid = new Grid
            {
                RowSpacing = 1,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(40, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(30, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(30, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(30, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(30, GridUnitType.Absolute) },
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                }
            };

            grid.Children.Add(foodDescriptionLabel, 0, 0);
            Grid.SetColumnSpan(foodDescriptionLabel, 3);

            grid.Children.Add(quantityTitleLabel, 0, 1);
            grid.Children.Add(quantityDetailsLabel, 0, 2);

            grid.Children.Add(mealTimeTitleLabel, 1, 1);
            grid.Children.Add(mealTimeDetailsLabel, 1, 2);

            grid.Children.Add(caloriesTitleLabel, 2, 1);
            grid.Children.Add(caloriesDetailsLabel, 2, 2);

            grid.Children.Add(proteinTitleLabel, 0, 3);
            grid.Children.Add(proteinDetailsLabel, 0, 4);

            grid.Children.Add(fatTitleLabel, 1, 3);
            grid.Children.Add(fatDetailsLabel, 1, 4);

            grid.Children.Add(carbsTitleLabel, 2, 3);
            grid.Children.Add(carbsDetailsLabel, 2, 4);

            View = new Frame
            {
                BackgroundColor = ColorConstants.Aqua,
                Margin = new Thickness(20),
                Content = grid,
                HasShadow = true
            };
        }
        #endregion

        #region Classes
        class FoodViewCellTitleLabel : Label
        {
            public FoodViewCellTitleLabel()
            {
                TextColor = ColorConstants.DarkMagenta;
                HorizontalTextAlignment = TextAlignment.Center;
                VerticalTextAlignment = TextAlignment.Center;
                FontAttributes = FontAttributes.Bold;
                Margin = new Thickness(0);
            }
        }

        class FoodViewCellDetailsLabel : Label
        {
            public FoodViewCellDetailsLabel()
            {
                TextColor = ColorConstants.DarkMagenta;
                HorizontalTextAlignment = TextAlignment.Center;
                VerticalTextAlignment = TextAlignment.Start;
                Margin = new Thickness(0);
            }
        }
        #endregion
    }
}
