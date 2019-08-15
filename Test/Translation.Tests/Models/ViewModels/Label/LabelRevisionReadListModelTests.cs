using NUnit.Framework;
using Shouldly;
using Translation.Client.Web.Models.Label;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Label
{
    [TestFixture]
    public sealed class LabelRevisionReadListModelTests
    {
        public LabelRevisionReadListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLabelRevisionReadListModel();
        }

        [Test]
        public void LabelRevisionReadListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "label_revision_list_title");
        }
        [Test]
        public void LabelRevisionReadListModel_Parameter()
        {
           SystemUnderTest.LabelName.ShouldBe(StringOne);
           SystemUnderTest.LabelUid.ShouldBe(UidOne);
        }
    }
}