using NUnit.Framework;

using Xamarin.UITest;

namespace HealthClinic.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public abstract class BaseTest
    {
        #region Constant Fields
        readonly Platform _platform;
        #endregion

        #region Constructors
        protected BaseTest(Platform platform) => _platform = platform;
        #endregion

        #region Properties
        protected IApp App { get; private set; }
        protected FoodListPage FoodListPage { get; private set; }
        protected AddFoodPage AddFoodPage { get; private set; }
        #endregion

        #region Methods
        [SetUp]
        public virtual void TestSetup()
        {
            App = AppInitializer.StartApp(_platform);

            FoodListPage = new FoodListPage(App);
            AddFoodPage = new AddFoodPage(App);

            App.Screenshot("App Launched");

            BackdoorMethodServices.DeleteTestFoodFromAPI(App);
        }

        [TearDown]
        public virtual void TestTearDown()
        {
        }
        #endregion
    }

}
