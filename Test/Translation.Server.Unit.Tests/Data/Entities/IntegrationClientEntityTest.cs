using NUnit.Framework;
using Shouldly;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

using Translation.Data.Entities.Main;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Server.Unit.Tests.Data.Entities
{

    [TestFixture]
    public class IntegrationClientEntityTests
    {
        [Test]
        public void IntegrationClient()
        {
            var entity = new IntegrationClient();

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

            AssertGuidProperty(properties, "ClientId", entity.ClientId);
            AssertGuidProperty(properties, "ClientSecret", entity.ClientSecret);
            AssertStringProperty(properties, "Description", entity.Description);
            AssertBooleanProperty(properties, "IsActive", entity.IsActive);
        }
    }
}