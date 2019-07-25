using NUnit.Framework;

using Translation.Client.Web.Models.Admin;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Admin
{
    [TestFixture]
    public sealed class SendEmailLogListModelTests
    {
        public SendEmailLogListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetSendEmailLogListModel();
        }

        [Test]
        public void SendEmailLogListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "send_email_log_list_title");
        }
    }
}