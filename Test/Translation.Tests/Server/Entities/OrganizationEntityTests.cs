using NUnit.Framework;
using Shouldly;

using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;
using Translation.Data.Entities.Main;
using static Translation.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Tests.Server.Entities
{
    [TestFixture]
    public class OrganizationEntityTests
    {
        [Test]
        public void Organization()
        {
            var entity = new Organization();

            var entityType = entity.GetType();
            var properties = entityType.GetProperties();

            entityType.BaseType.Name.ShouldBe(nameof(BaseEntity));
            entityType.GetInterface(nameof(ISchemaMain)).ShouldNotBeNull();

            AssertStringProperty(properties, "Description", entity.Description);
            AssertBooleanProperty(properties, "IsActive", entity.IsActive);
            AssertStringProperty(properties, "ObfuscationKey", entity.ObfuscationKey);
            AssertStringProperty(properties,"ObfuscationIv",entity.ObfuscationIv);

            AssertIntegerProperty(properties, "UserCount",entity.UserCount);
            AssertIntegerProperty(properties, "ProjectCount", entity.ProjectCount);
            AssertIntegerProperty(properties, "LabelCount", entity.LabelCount);
            AssertIntegerProperty(properties, "LabelTranslationCount", entity.LabelTranslationCount);

            AssertBooleanProperty(properties, "IsSuperOrganization", entity.IsSuperOrganization);
        }
    }
}