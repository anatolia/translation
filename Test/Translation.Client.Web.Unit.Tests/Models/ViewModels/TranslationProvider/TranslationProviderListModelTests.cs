using NUnit.Framework;

using Translation.Client.Web.Models.TranslationProvider;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.TranslationProvider
{
    [TestFixture]
    public sealed class TranslationProviderListModelTests
    {
        public TranslationProviderListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetTranslationProviderListModel();
        }

        [Test]
        public void TranslationProviderListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "translation_provider_list_title");
        }
    }
}