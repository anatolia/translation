using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Project;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Project
{
    [TestFixture]
    public class ProjectPendingTranslationReadListModelTests
    {
        public ProjectPendingTranslationReadListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetProjectPendingTranslationReadListModel(UidOne, StringOne);
        }

        [Test]
        public void ProjectPendingTranslationReadListModel_Parameters()
        {
            SystemUnderTest.ProjectUid.ShouldBe(UidOne);
            SystemUnderTest.ProjectName.ShouldBe(StringOne);
        }

        [Test]
        public void ProjectPendingTranslationReadListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "project_pending_translations_title");
        }
    }
}
