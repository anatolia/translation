using NUnit.Framework;
using Shouldly;
using StandardUtils.Models.DataTransferObjects;

using Translation.Common.Models.DataTransferObjects;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Common.Tests.Models.DataTransferObjects
{
    [TestFixture]
    public class LabelTranslationDtoTests
    {
        [Test]
        public void LabelTranslationDto()
        {
            var dto = new LabelTranslationDto();

            var dtoType = dto.GetType();
            var properties = dtoType.GetProperties();

            dtoType.BaseType.Name.ShouldBe(nameof(BaseDto));

            AssertGuidProperty(properties, "OrganizationUid", dto.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", dto.OrganizationName);

            AssertGuidProperty(properties, "ProjectUid", dto.ProjectUid);
            AssertStringProperty(properties, "ProjectName", dto.ProjectName);

            AssertGuidProperty(properties, "LabelUid", dto.LabelUid);
            AssertStringProperty(properties, "LabelKey", dto.LabelKey);

            AssertGuidProperty(properties, "LanguageUid", dto.LanguageUid);
            AssertStringProperty(properties, "LanguageName", dto.LanguageName);
            AssertStringProperty(properties, "LanguageIsoCode2", dto.LanguageIsoCode2);
            AssertStringProperty(properties, "LanguageIconUrl", dto.LanguageIconUrl);

            AssertStringProperty(properties, "Translation", dto.Translation);
        }
    }
}