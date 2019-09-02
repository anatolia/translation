using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Base;
using Translation.Common.Models.DataTransferObjects;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Common.Tests.Models.DataTransferObjects
{
    [TestFixture]
    public class TokenRequestLogDtoTests
    {
        [Test]
        public void TokenRequestLogDto()
        {
            var dto = new TokenRequestLogDto();

            var dtoType = dto.GetType();
            var properties = dtoType.GetProperties();

            dtoType.BaseType.Name.ShouldBe(nameof(BaseDto));

            AssertGuidProperty(properties, "OrganizationUid", dto.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", dto.OrganizationName);

            AssertGuidProperty(properties, "IntegrationUid", dto.IntegrationUid);
            AssertStringProperty(properties, "IntegrationName", dto.IntegrationName);

            AssertGuidProperty(properties, "IntegrationClientUid", dto.IntegrationClientUid);
            AssertStringProperty(properties, "IntegrationClientName", dto.IntegrationClientName);

            AssertGuidProperty(properties, "TokenUid", dto.TokenUid);
            AssertStringProperty(properties, "TokenName", dto.TokenName);

            AssertStringProperty(properties, "Ip", dto.Ip);
            AssertStringProperty(properties, "Country", dto.Country);
            AssertStringProperty(properties, "City", dto.City);
            AssertStringProperty(properties, "HttpMethod", dto.HttpMethod);
            AssertStringProperty(properties, "Request", dto.Request);
            AssertStringProperty(properties, "Response", dto.Response);
            AssertStringProperty(properties, "ResponseCode", dto.ResponseCode);


        }
    }
}