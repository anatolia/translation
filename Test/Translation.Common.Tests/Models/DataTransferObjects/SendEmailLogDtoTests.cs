using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Base;
using Translation.Common.Models.DataTransferObjects;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Common.Tests.Models.DataTransferObjects
{
    [TestFixture]
    public class SendEmailLogDtoTests
    {
        [Test]
        public void SendEmailLogDto()
        {
            var dto = new SendEmailLogDto();

            var dtoType = dto.GetType();
            var properties = dtoType.GetProperties();

            dtoType.BaseType.Name.ShouldBe(nameof(BaseDto));

            AssertGuidProperty(properties, "OrganizationUid", dto.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", dto.OrganizationName);

            AssertGuidProperty(properties, "MailUid", dto.MailUid);

            AssertStringProperty(properties, "Subject", dto.Subject);
            AssertStringProperty(properties, "EmailFrom", dto.EmailFrom);
            AssertStringProperty(properties, "EmailTo", dto.EmailTo);

            AssertBooleanProperty(properties, "IsOpened", dto.IsOpened);

        }
    }
}