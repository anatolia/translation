using NUnit.Framework;
using Shouldly;
using StandardRepository.Models.Entities;
using StandardRepository.Models.Entities.Schemas;

using Translation.Data.Entities.Parameter;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Server.Unit.Tests.Data.Entities
{
    [TestFixture]
    public class WordEntityTests
    {
        [Test]
        public void Word()
        {
            var entity = new Word();

            var entityType = entity.GetType();
            var properties = entityType.GetProperties();

            entityType.BaseType.Name.ShouldBe(nameof(BaseEntity));
            entityType.GetInterface(nameof(ISchemaParameter)).ShouldNotBeNull();

            AssertLongProperty(properties, "LanguageId", entity.LanguageId);
            AssertGuidProperty(properties, "LanguageUid", entity.LanguageUid);
            AssertStringProperty(properties, "LanguageName", entity.LanguageName);
        }
    }
}