using NUnit.Framework;
using Shouldly;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

using Translation.Data.Entities.Parameter;
using static Translation.Tests.TestHelpers.AssertPropertyTestHelper;
namespace Translation.Tests.Server.Entities
{
    [TestFixture]
    public class LanguageEntityTests
    {
        [Test]
        public void Language()
        {
            var entity = new Language();

            var entityType = entity.GetType();
            var properties = entityType.GetProperties();

            entityType.BaseType.Name.ShouldBe(nameof(BaseEntity));
            entityType.GetInterface(nameof(ISchemaParameter)).ShouldNotBeNull();

            AssertStringProperty(properties, "IsoCode2Char",entity.IsoCode2Char);

            AssertStringProperty(properties, "IsoCode3Char", entity.IsoCode3Char);
            AssertStringProperty(properties, "OriginalName", entity.OriginalName);
            AssertStringProperty(properties, "Description", entity.Description);
            AssertStringProperty(properties, "IconUrl", entity.IconUrl);
        }
    }
}