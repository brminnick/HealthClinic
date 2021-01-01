using System;
using HealthClinic.Shared;
using NUnit.Framework;

using Xamarin.UITest;

namespace HealthClinic.UITests
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    abstract class BaseTest
    {
        readonly Platform _platform;

        IApp? _app;
        AddFoodPage? _addFoodPage;
        FoodListPage? _foodListPage;

        protected BaseTest(Platform platform) => _platform = platform;

        protected IApp App => _app ?? throw new NullReferenceException();
        protected AddFoodPage AddFoodPage => _addFoodPage ?? throw new NullReferenceException();
        protected FoodListPage FoodListPage => _foodListPage ?? throw new NullReferenceException();

        [SetUp]
        public virtual void TestSetup()
        {
            _app = AppInitializer.StartApp(_platform);

            _addFoodPage = new AddFoodPage(App);
            _foodListPage = new FoodListPage(App);

            App.Screenshot("App Launched");

            App.InvokeBackdoorMethod(BackdoorMethodConstants.DeleteTestFoodFromAPI);
        }

        [TearDown]
        public virtual void TestTearDown()
        {
        }
    }
}
