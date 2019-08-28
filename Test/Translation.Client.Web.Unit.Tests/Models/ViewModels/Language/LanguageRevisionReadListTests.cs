using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Language;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Language
{
    [TestFixture]
    public class LanguageRevisionReadListTests
    {
        public LanguageRevisionReadListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLanguageRevisionReadListModel();
        }

        [Test]
        public void LanguageRevisionReadListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "language_revision_list_title");
        }

        [Test]
        public void LanguageRevisionReadListModel_Parameter()
        {
          SystemUnderTest.LanguageUid.ShouldBe(UidOne);
          SystemUnderTest.LanguageName.ShouldBe(StringOne);
        }

    }
}
