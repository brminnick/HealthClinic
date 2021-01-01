using System;
using Xamarin.UITest;

namespace HealthClinic.UITests
{
    static class AppInitializer
    {
        public static IApp StartApp(Platform platform) => platform switch
        {
            Platform.Android => ConfigureApp.Android.PreferIdeSettings().StartApp(),
            Platform.iOS => ConfigureApp.iOS.PreferIdeSettings().StartApp(),
            _ => throw new NotSupportedException("Platform Not Supported"),
        };
    }
}
