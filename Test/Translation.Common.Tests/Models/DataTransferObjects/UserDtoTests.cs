using NUnit.Framework;
using Shouldly;
using StandardUtils.Models.DataTransferObjects;

using Translation.Common.Models.DataTransferObjects;
using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Common.Tests.Models.DataTransferObjects
{
    [TestFixture]
    public class UserDtoTests
    {
        [Test]
        public void UserDto()
        {
            var dto = new UserDto();

            var dtoType = dto.GetType();
            var properties = dtoType.GetProperties();

            dtoType.BaseType.Name.ShouldBe(nameof(BaseDto));

            AssertGuidProperty(properties, "OrganizationUid", dto.OrganizationUid);
            AssertStringProperty(properties, "OrganizationName", dto.OrganizationName);

            AssertStringProperty(properties, "Email", dto.Email);
            AssertStringProperty(properties, "FirstName", dto.FirstName);
            AssertStringProperty(properties, "LastName", dto.LastName);
            AssertStringProperty(properties, "Description", dto.Description);
            AssertBooleanProperty(properties, "IsActive", dto.IsActive);
            AssertBooleanProperty(properties, "IsAdmin", dto.IsAdmin);
            AssertBooleanProperty(properties, "IsSuperAdmin", dto.IsSuperAdmin);
            AssertNullableDateTimeProperty(properties, "LastLoggedInAt", dto.LastLoggedInAt);

            AssertNullableDateTimeProperty(properties, "InvitedAt", dto.InvitedAt);
            AssertNullableGuidProperty(properties, "InvitedByUserUid", dto.InvitedByUserUid);
            AssertStringProperty(properties, "InvitedByUserName", dto.InvitedByUserName);

            AssertIntegerProperty(properties, "LabelCount", dto.LabelCount);
            AssertIntegerProperty(properties, "LabelTranslationCount", dto.LabelTranslationCount);

            AssertGuidProperty(properties, "LanguageUid", dto.LanguageUid);
            AssertStringProperty(properties, "LanguageName", dto.LanguageName);
            AssertStringProperty(properties, "LanguageIconUrl", dto.LanguageIconUrl);
        }
    }
}