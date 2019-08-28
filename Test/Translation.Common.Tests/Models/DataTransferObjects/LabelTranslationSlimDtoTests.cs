using NUnit.Framework;
using Translation.Common.Models.DataTransferObjects;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Common.Tests.Models.DataTransferObjects
{
    [TestFixture]
    public class LabelTranslationSlimDtoTests
    {
        [Test]
        public void LabelTranslationSlimDto()
        {
            var dto = new LabelTranslationSlimDto();

            var dtoType = dto.GetType();
            var properties = dtoType.GetProperties();

            AssertStringProperty(properties, "LanguageIsoCode2", dto.LanguageIsoCode2);
            AssertStringProperty(properties, "Translation", dto.Translation);

        }
    }
}