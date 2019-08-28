using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Base;
using Translation.Common.Models.DataTransferObjects;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Common.Tests.Models.DataTransferObjects
{
    [TestFixture]
    public class LabelDtoTests
    {
        [Test]
        public void LabelDto()
        {
            var dto = new LabelDto();

            var dtoType = dto.GetType();
            var properties = dtoType.GetProperties();

            dtoType.BaseType.Name.ShouldBe(nameof(BaseDto));
            
            AssertGuidProperty(properties, "OrganizationUid", dto.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", dto.OrganizationName);

            AssertGuidProperty(properties, "ProjectUid", dto.ProjectUid);
            AssertStringProperty(properties, "ProjectName", dto.ProjectName);

            AssertStringProperty(properties, "Key", dto.Key);
            AssertStringProperty(properties, "Description", dto.Description);
            AssertBooleanProperty(properties, "IsActive", dto.IsActive);

            AssertIntegerProperty(properties, "LabelTranslationCount", dto.LabelTranslationCount);
        }
    }
}