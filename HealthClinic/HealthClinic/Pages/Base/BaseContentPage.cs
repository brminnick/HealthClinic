using Xamarin.Forms;

namespace HealthClinic
{
    public abstract class BaseContentPage<T> : ContentPage where T : BaseViewModel, new()
    {
        protected BaseContentPage(string pageTitle)
        {
            Title = pageTitle;
            BindingContext = ViewModel;
            BackgroundColor = ColorConstants.Aqua;
        }

        protected T ViewModel { get; } = new T();
    }
}
