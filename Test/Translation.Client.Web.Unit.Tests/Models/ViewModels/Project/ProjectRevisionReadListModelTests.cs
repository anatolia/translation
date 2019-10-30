using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Project;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Project
{
    [TestFixture]
    public class ProjectRevisionReadListModelTests
    {
        public ProjectRevisionReadListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetProjectRevisionReadListModel(UidOne, StringOne);
        }

        [Test]
        public void ProjectRevisionReadListModel_Parameters()
        {
            SystemUnderTest.ProjectUid.ShouldBe(UidOne);
            SystemUnderTest.ProjectName.ShouldBe(StringOne);
        }

        [Test]
        public void ProjectRevisionReadListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "project_revision_list_title");
        }
    }
}
