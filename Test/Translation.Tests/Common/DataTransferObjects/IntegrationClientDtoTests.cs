using NUnit.Framework;

using Shouldly;

using Translation.Common.Models.Base;
using Translation.Common.Models.DataTransferObjects;
using static Translation.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Tests.Common.DataTransferObjects
{
    [TestFixture]
    public class IntegrationClientDtoTests
    {
        [Test]
        public void IntegrationClientDto()
        {
            var dto = new IntegrationClientDto();

            var dtoType = dto.GetType();
            var properties = dtoType.GetProperties();

            dtoType.BaseType.Name.ShouldBe(nameof(BaseDto));

            AssertGuidProperty(properties, "OrganizationUid", dto.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", dto.OrganizationName);

            AssertGuidProperty(properties, "IntegrationUid", dto.IntegrationUid);
            AssertStringProperty(properties, "IntegrationName", dto.IntegrationName);

            AssertGuidProperty(properties, "ClientId", dto.ClientId);
            AssertGuidProperty(properties, "ClientSecret", dto.ClientSecret);
            AssertBooleanProperty(properties, "IsActive", dto.IsActive);
        }
    }
}