using NUnit.Framework;
using Shouldly;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

using Translation.Data.Entities.Main;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;


namespace Translation.Server.Unit.Tests.Data.Entities
{
    [TestFixture]
    public class TokenEntityTests
    {
        [Test]
        public void Token()
        {
            var entity = new Token();

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

            AssertGuidProperty(properties, "AccessToken", entity.AccessToken);
            AssertDateTimeProperty(properties, "ExpiresAt", entity.ExpiresAt);
            AssertStringProperty(properties, "Ip", entity.Ip);
            AssertBooleanProperty(properties, "IsActive", entity.IsActive);
        }
    }
}