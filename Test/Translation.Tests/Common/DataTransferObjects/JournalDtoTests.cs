using NUnit.Framework;

using Shouldly;

using Translation.Common.Models.Base;
using Translation.Common.Models.DataTransferObjects;
using static Translation.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Tests.Common.DataTransferObjects
{
    [TestFixture]
    public class JournalDtoTests
    {
        [Test]
        public void JournalDto()
        {
            var dto = new JournalDto();

            var dtoType = dto.GetType();
            var properties = dtoType.GetProperties();

            dtoType.BaseType.Name.ShouldBe(nameof(BaseDto));

            AssertGuidProperty(properties, "OrganizationUid", dto.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", dto.OrganizationName);

            AssertGuidProperty(properties, "IntegrationUid", dto.IntegrationUid);
            AssertStringProperty(properties, "IntegrationName", dto.IntegrationName);

            AssertGuidProperty(properties, "UserUid", dto.UserUid);
            AssertStringProperty(properties, "UserName", dto.UserName);

            AssertStringProperty(properties, "Message", dto.Message);

        }
    }
}