using NUnit.Framework;
using Xamarin.UITest;

namespace HealthClinic.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    class ReplTests
    {
        readonly Platform _platform;

        IApp? _app;

        public ReplTests(Platform platform) => _platform = platform;

        [SetUp]
        public void TestSetup() => _app = AppInitializer.StartApp(_platform);


        [Test, Ignore("REPL only used for writing tests")]
        public void ReplTest() => _app?.Repl();
    }
}
