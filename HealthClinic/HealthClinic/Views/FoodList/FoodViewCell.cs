using HealthClinic.Shared;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using static HealthClinic.MarkupExtensions;
using static Xamarin.CommunityToolkit.Markup.GridRowsColumns;

namespace HealthClinic
{
    public class FoodViewCell : ViewCell
    {
        public FoodViewCell()
        {
            View = new Frame
            {
                HasShadow = true,
                Margin = new Thickness(20),
                BackgroundColor = ColorConstants.Aqua,

                Content = new Grid
                {
                    RowSpacing = 1,
                    RowDefinitions = Rows.Define(
                        (Row.Food, AbsoluteGridLength(40)),
                        (Row.Title1, AbsoluteGridLength(30)),
                        (Row.Description1, AbsoluteGridLength(30)),
                        (Row.Title2, AbsoluteGridLength(30)),
                        (Row.Description2, AbsoluteGridLength(30))),

                    ColumnDefinitions = Columns.Define(
                        (Column.Column1, Star),
                        (Column.Column2, Star),
                        (Column.Column3, Star)),

                    Children =
                    {
                        new Label { TextColor = ColorConstants.Maroon }.TextCenterHorizontal().TextTop().Font(size: 20, bold: true)
                            .Row(Row.Food).ColumnSpan(All<Column>())
                            .Bind(Label.TextProperty, nameof(FoodLogModel.Description_PascalCase)),

                        new FoodViewCellTitleLabel { Text = "Quantity" }
                            .Row(Row.Title1).Column(Column.Column1),

                        new FoodViewCellDetailsLabel()
                            .Row(Row.Description1).Column(Column.Column1)
                            .Bind(Label.TextProperty, nameof(FoodLogModel.Quantity)),

                        new FoodViewCellTitleLabel { Text = "Meal Time" }
                            .Row(Row.Title1).Column(Column.Column2),

                        new FoodViewCellDetailsLabel()
                            .Row(Row.Description1).Column(Column.Column2)
                            .Bind(Label.TextProperty, nameof(FoodLogModel.MealTime_Formatted)),

                        new FoodViewCellTitleLabel { Text = "Calories" }
                            .Row(Row.Title1).Column(Column.Column3),

                        new FoodViewCellDetailsLabel()
                            .Row(Row.Description1).Column(Column.Column3)
                            .Bind(Label.TextProperty, nameof(FoodLogModel.Calories)),

                        new FoodViewCellTitleLabel { Text = "Protein" }
                            .Row(Row.Title2).Column(Column.Column1),

                        new FoodViewCellDetailsLabel()
                            .Row(Row.Description2).Column(Column.Column1)
                            .Bind(Label.TextProperty, nameof(FoodLogModel.Protein_Formatted)),


                        new FoodViewCellTitleLabel { Text = "Fat" }
                            .Row(Row.Title2).Column(Column.Column2),

                        new FoodViewCellDetailsLabel()
                            .Row(Row.Description2).Column(Column.Column2)
                            .Bind(Label.TextProperty, nameof(FoodLogModel.Fat_Formatted)),

                        new FoodViewCellTitleLabel { Text = "Carbs" }
                            .Row(Row.Title2).Column(Column.Column3),

                        new FoodViewCellDetailsLabel()
                            .Row(Row.Description2).Column(Column.Column3)
                            .Bind(Label.TextProperty, nameof(FoodLogModel.Carbohydrates_Formatted))
                    }
                }
            };
        }

        enum Row { Food, Title1, Description1, Title2, Description2 }
        enum Column { Column1, Column2, Column3 }

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
    }

    static class MarkupExtensions
    {
        public static GridLength AbsoluteGridLength(double value) => new GridLength(value, GridUnitType.Absolute);
        public static GridLength AbsoluteGridLength(int value) => AbsoluteGridLength((double)value);
    }
}