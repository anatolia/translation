using Translation.Common.Models.DataTransferObjects;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.TestHelpers
{
    public class FakeDtoTestHelper
    {
        public static UserDto GetUserDto()
        {
            var dto = new UserDto();
            dto.OrganizationUid = UidOne;
            dto.OrganizationName = StringOne;

            dto.Uid = UidTwo;
            dto.Name = StringTwo;

            dto.FirstName = StringThree;
            dto.LastName = StringFour;
            dto.Description = StringFive;
            dto.Email = OrganizationOneUserOneEmail;
            dto.IsActive = BooleanTrue;
            dto.IsAdmin = BooleanFalse;
            dto.IsSuperAdmin = BooleanFalse;
            dto.LastLoggedInAt = DateTimeOne;

            dto.InvitedAt = DateTimeTwo;
            dto.InvitedByUserUid = UidThree;

            dto.LabelCount = One;
            dto.LabelTranslationCount = Two;

            dto.LanguageUid = UidFour;
            dto.LanguageName = StringSix;
            dto.LanguageIconUrl = StringSeven;

            return dto;
        }

        public static ProjectDto GetProjectDto()
        {
            var dto = new ProjectDto();
            dto.Uid = UidOne;
            dto.Name = StringOne;

            dto.OrganizationUid = UidTwo;
            dto.OrganizationName = StringTwo;

            dto.LabelCount = One;
            dto.LabelTranslationCount = Two;

            dto.IsActive = BooleanTrue;

            dto.LanguageUid = UidTwo;
            dto.LanguageName = StringTwo;
            dto.LanguageIconUrl = HttpsUrl;

            return dto;
        }

        public static OrganizationDto GetOrganizationDto()
        {
            var dto = new OrganizationDto();
            dto.Uid = UidOne;
            dto.Name = StringOne;
            dto.CreatedAt = DateTimeOne;
            dto.Description = StringTwo;
            dto.IsActive = BooleanTrue;

            return dto;
        }

        public static LanguageDto GetLanguageDto()
        {
            var dto = new LanguageDto();
            dto.Name = StringOne;
            dto.Uid = UidOne;
            dto.OriginalName = StringTwo;
            dto.IsoCode2 = IsoCode2One;
            dto.IsoCode3 = IsoCode3One;
            dto.Description = StringThree;

            return dto;
        }

        public static LabelDto GetLabelDto()
        {
            var dto = new LabelDto();
            dto.OrganizationUid = UidOne;
            dto.OrganizationName = StringOne;

            dto.ProjectUid = UidTwo;
            dto.ProjectName = StringTwo;

            dto.Uid = UidThree;
            dto.Name = StringThree;
            dto.Key = StringFour;

            dto.IsActive = BooleanTrue;

            return dto;
        }

        public static LabelTranslationDto GetLabelTranslationDto()
        {
            var dto = new LabelTranslationDto();
            dto.OrganizationUid = UidOne;
            dto.OrganizationName = StringOne;

            dto.ProjectUid = UidTwo;
            dto.ProjectName = StringTwo;

            dto.LabelUid = UidThree;

            dto.Uid = UidThree;
            dto.Name = StringThree;

            return dto;
        }

        public static IntegrationDto GetIntegrationDto()
        {
            var dto = new IntegrationDto();
            dto.OrganizationUid = UidOne;
            dto.OrganizationName = StringOne;

            dto.Uid = UidTwo;
            dto.Name = StringTwo;

            dto.CreatedAt = DateTimeOne;
            dto.IsActive = BooleanTrue;

            return dto;
        }

        public static IntegrationClientDto GetIntegrationClientDto()
        {
            var dto = new IntegrationClientDto();
            dto.OrganizationUid = UidOne;
            dto.OrganizationName = StringOne;

            dto.IntegrationUid = UidTwo;
            dto.IntegrationName = StringTwo;

            dto.ClientId = UidThree;
            dto.Uid = UidThree;

            dto.Name = StringThree;
            dto.CreatedAt = DateTimeOne;
            dto.IsActive = BooleanTrue;

            return dto;
        }
    }
}