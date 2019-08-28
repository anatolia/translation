using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Base;
using Translation.Common.Models.DataTransferObjects;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Common.Tests.Models.DataTransferObjects
{
    [TestFixture]
    public class OrganizationDtoTests
    {
        [Test]
        public void Organization()
        {
            var dto = new OrganizationDto();

            var dtoType = dto.GetType();
            var properties = dtoType.GetProperties();

            dtoType.BaseType.Name.ShouldBe(nameof(BaseDto));

            AssertStringProperty(properties, "Description", dto.Description);
            AssertBooleanProperty(properties, "IsActive", dto.IsActive);
          

            AssertIntegerProperty(properties, "UserCount", dto.UserCount);
            AssertIntegerProperty(properties, "ProjectCount", dto.ProjectCount);
            AssertIntegerProperty(properties, "LabelCount", dto.LabelCount);
            AssertIntegerProperty(properties, "LabelTranslationCount", dto.LabelTranslationCount);

            
        }
    }
}