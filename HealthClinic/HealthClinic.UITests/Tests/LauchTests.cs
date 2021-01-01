using NUnit.Framework;

using Xamarin.UITest;

namespace HealthClinic.UITests
{
    class LauchTests : BaseTest
    {
        public LauchTests(Platform platform) : base(platform)
        {
        }

        [Test]
        public void LaunchTest() => FoodListPage.WaitForPageToLoad();
    }
}
