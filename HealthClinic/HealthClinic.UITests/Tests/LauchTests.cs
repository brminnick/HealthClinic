using NUnit.Framework;

using Xamarin.UITest;

namespace HealthClinic.UITests
{
    public class LauchTests : BaseTest
    {
        public LauchTests(Platform platform) : base(platform)
        {
        }

        [Test]
        public void LaunchTest() => FoodListPage.WaitForPageToLoad();
    }
}
