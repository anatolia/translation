using NUnit.Framework;

using Translation.Client.Web.Models.Admin;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Admin
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