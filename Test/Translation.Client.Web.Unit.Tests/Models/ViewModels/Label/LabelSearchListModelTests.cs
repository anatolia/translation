using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Label;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Label
{
    [TestFixture]
    public sealed class LabelSearchListModelTests
    {
        public LabelSearchListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLabelSearchListModel();
        }

        [Test]
        public void LabelSearchListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "label_search_list_title");
        }
        [Test]
        public void LabelSearchListModel_Parameter()
        {
          SystemUnderTest.SearchTerm.ShouldBe(StringOne);
        }
    }
}