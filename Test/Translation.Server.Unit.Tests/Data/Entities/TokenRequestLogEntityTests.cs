using NUnit.Framework;
using Shouldly;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

using Translation.Data.Entities.Main;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;


namespace Translation.Server.Unit.Tests.Data.Entities
{
    [TestFixture]
    public class TokenRequestLogEntityTests
    {
        [Test]
        public void TokenRequestLog()
        {
            var entity = new TokenRequestLog();

            var entityType = entity.GetType();
            var properties = entityType.GetProperties();

            entityType.BaseType.Name.ShouldBe(nameof(BaseEntity));
            entityType.GetInterface(nameof(ISchemaMain)).ShouldNotBeNull();

            AssertLongProperty(properties, "OrganizationId", entity.OrganizationId);
            AssertGuidProperty(properties, "OrganizationUid", entity.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", entity.OrganizationName);

            AssertLongProperty(properties, "IntegrationId", entity.IntegrationId);
            AssertGuidProperty(properties, "IntegrationUid", entity.IntegrationUid);
            AssertStringProperty(properties, "IntegrationName", entity.IntegrationName);

            AssertLongProperty(properties, "IntegrationClientId", entity.IntegrationClientId);
            AssertGuidProperty(properties, "IntegrationClientUid", entity.IntegrationClientUid);
            AssertStringProperty(properties, "IntegrationClientName", entity.IntegrationClientName);

            AssertLongProperty(properties, "TokenId", entity.TokenId);
            AssertGuidProperty(properties, "TokenUid", entity.TokenUid);
            AssertStringProperty(properties, "TokenName", entity.TokenName);

            AssertStringProperty(properties, "Ip", entity.Ip);
            AssertStringProperty(properties, "Country", entity.Country);
            AssertStringProperty(properties, "City", entity.City);
            AssertStringProperty(properties, "HttpMethod", entity.HttpMethod);
            AssertStringProperty(properties, "Request", entity.Request);
            AssertStringProperty(properties, "Response", entity.Response);
            AssertStringProperty(properties, "ResponseCode", entity.ResponseCode);
        }
    }
}