using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.TranslationProvider;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.TranslationProvider
{
    [TestFixture]
    public class TranslationProviderDetailModelTests
    {
        public TranslationProviderDetailModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetTranslationProviderDetailModel();
        }

        [Test]
        public void TranslationProviderDetailModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "translation_provider_detail_title");
        }

        [Test]
        public void TranslationProviderDetailModel_Parameter()
        {
            SystemUnderTest.TranslationProviderUid.ShouldBe(UidOne);
            SystemUnderTest.Value.ShouldBe(StringOne);
            SystemUnderTest.Name.ShouldBe(StringTwo);
            SystemUnderTest.Description.ShouldBe(StringThree);
        }
    }
}
