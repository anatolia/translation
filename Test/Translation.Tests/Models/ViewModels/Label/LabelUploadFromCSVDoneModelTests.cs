using NUnit.Framework;

using Translation.Client.Web.Models.Label;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Label
{
    [TestFixture]
    public sealed class LabelUploadFromCSVDoneModelTests
    {
        public LabelUploadFromCSVDoneModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLabelUploadFromCSVDoneModel();
        }

        [Test]
        public void LabelUploadFromCSVDoneModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "label_upload_done_title");
        }
    }
}