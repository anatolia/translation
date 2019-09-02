using NUnit.Framework;
using Shouldly;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

using Translation.Data.Entities.Main;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Server.Unit.Tests.Data.Entities
{
    [TestFixture]
    public class IntegrationEntityTests
    {
        [Test]
        public void Integration()
        {
            var entity = new Integration();

            var entityType = entity.GetType();
            var properties = entityType.GetProperties();

            entityType.BaseType.Name.ShouldBe(nameof(BaseEntity));
            entityType.GetInterface(nameof(ISchemaMain)).ShouldNotBeNull();

            AssertLongProperty(properties, "OrganizationId", entity.OrganizationId);
            AssertGuidProperty(properties, "OrganizationUid", entity.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", entity.OrganizationName);

            AssertStringProperty(properties, "Description", entity.Description);
            AssertBooleanProperty(properties, "IsActive", entity.IsActive);
        }
    }
}