using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Base;
using Translation.Common.Models.DataTransferObjects;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Common.Tests.Models.DataTransferObjects
{
    [TestFixture]
    public class ProjectDtoTests
    {
        [Test]
        public void ProjectDto()
        {
            var dto = new ProjectDto();

            var dtoType = dto.GetType();
            var properties = dtoType.GetProperties();

            dtoType.BaseType.Name.ShouldBe(nameof(BaseDto));

            AssertGuidProperty(properties, "OrganizationUid", dto.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", dto.OrganizationName);

            AssertStringProperty(properties, "Description", dto.Description);
            AssertStringProperty(properties, "Url", dto.Url);
            AssertBooleanProperty(properties, "IsActive", dto.IsActive);
            AssertIntegerProperty(properties, "LabelCount", dto.LabelCount);
            AssertIntegerProperty(properties, "LabelTranslationCount", dto.LabelTranslationCount);
            AssertBooleanProperty(properties, "IsSuperProject", dto.IsSuperProject);
            AssertStringProperty(properties, "Slug", dto.Slug);

            AssertLongProperty(properties, "LanguageId", dto.LanguageId);
            AssertGuidProperty(properties, "LanguageUid", dto.LanguageUid);
            AssertStringProperty(properties, "LanguageName", dto.LanguageName);
            AssertStringProperty(properties, "LanguageIconUrl", dto.LanguageIconUrl);
        }

    }
}