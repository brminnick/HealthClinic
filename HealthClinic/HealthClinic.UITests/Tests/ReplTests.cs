using Xamarin.UITest;

using NUnit.Framework;

namespace HealthClinic.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class ReplTests
    {
        #region Constant Fields
        readonly Platform _platform;
        #endregion

        #region Fields
        IApp _app;
        #endregion

        #region Constructors
        public ReplTests(Platform platform) => _platform = platform;
        #endregion

        #region Methods
        [SetUp]
        public void TestSetup() => _app = AppInitializer.StartApp(_platform);

        
        [Test, Ignore]
        public void ReplTest() => _app.Repl();
        #endregion
    }
}
