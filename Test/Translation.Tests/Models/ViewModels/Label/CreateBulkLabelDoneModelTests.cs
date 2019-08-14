using NUnit.Framework;

using Translation.Client.Web.Models.Label;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Label
{
    [TestFixture]
    public sealed class CreateBulkLabelDoneModelTests
    {
        public CreateBulkLabelDoneModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetCreateBulkLabelDoneModel();
        }

        [Test]
        public void CreateBulkLabelDoneModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "create_bulk_label_done_title");
        }
    }
}