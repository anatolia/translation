using System.Collections.Generic;

using Translation.Common.Models.DataTransferObjects;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.TestHelpers
{
    public class FakeDtoTestHelper
    {
        public static TranslationProviderDto GetTranslationProviderDto()
        {
            var dto = new TranslationProviderDto();

            dto.Uid = UidTwo;
            dto.Name = StringTwo;
            dto.Description = StringFive;
            dto.IsActive = BooleanTrue;

            return dto;
        }

        public static UserDto GetUserDto()
        {
            var dto = new UserDto();
            dto.OrganizationUid = OrganizationOneUid;
            dto.OrganizationName = OrganizationOneName;

            dto.Uid = OrganizationOneUserOneUid;
            dto.Name = OrganizationOneUserOneName;

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

            dto.OrganizationUid = OrganizationOneUid;
            dto.OrganizationName = StringOne;

            dto.Uid = OrganizationOneProjectOneUid;
            dto.Name = OrganizationOneProjectOneName;
            dto.Url = HttpsUrl;
            dto.Slug = OrganizationOneProjectOneSlug;
           
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
            dto.Uid = OrganizationOneUid;
            dto.Name = StringOne;
            dto.CreatedAt = DateTimeOne;
            dto.Description = StringTwo;
            dto.IsActive = BooleanTrue;

            return dto;
        }

        public static TokenDto GetTokenDto()
        {
            var dto = new TokenDto();
            dto.Uid = UidOne;
            dto.Name = StringOne;
            dto.CreatedAt = DateTimeOne;
            dto.AccessToken = UidTwo;
            dto.IntegrationClientUid = UidThree;
            dto.ExpiresAt = DateTimeOne;
            dto.IP = IpOne;
            return dto;
        }

        public static LanguageDto GetLanguageDtoOne()
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

        public static LanguageDto GetLanguageDtoTwo()
        {
            var dto = new LanguageDto();
            dto.Name = StringTwo;
            dto.Uid = UidTwo;
            dto.OriginalName = StringThree;
            dto.IsoCode2 = IsoCode2Two;
            dto.IsoCode3 = IsoCode3Two;
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

        public static JournalDto GetUserUidAndIntegrationUidEmptyJournalDto()
        {
            var dto = new JournalDto();
            dto.OrganizationUid = UidOne;
            dto.OrganizationName = StringOne;
            dto.UserUid = EmptyUid;
            dto.UserName = StringOne;
            dto.IntegrationUid = EmptyUid;
            dto.IntegrationName = StringTwo;
            dto.Message = StringFive;
            dto.CreatedAt = DateTimeOne;
            dto.Uid = UidThree;
            dto.Name = StringThree;

            return dto;
        }

        public static JournalDto GetJournalDto()
        {
            var dto = new JournalDto();
            dto.OrganizationUid = UidOne;
            dto.OrganizationName = StringOne;
            dto.UserUid = UidOne;
            dto.UserName = StringOne;
            dto.IntegrationUid = UidTwo;
            dto.IntegrationName = StringTwo;
            dto.Message = StringFive;
            dto.CreatedAt = DateTimeOne;
            dto.Uid = UidThree;
            dto.Name = StringThree;

            return dto;
        }

        public static SendEmailLogDto GetSendEmailLogDto()
        {
            var dto = new SendEmailLogDto();
            dto.OrganizationUid = UidOne;
            dto.OrganizationName = StringOne;
            dto.CreatedAt = DateTimeOne;
            dto.Uid = UidThree;
            dto.Name = StringThree;

            return dto;
        }

        public static LabelFatDto GetLabelFatDto()
        {
            var dto = new LabelFatDto();

            dto.Uid = UidThree;
            dto.Key = StringFour;
            dto.Translations = new List<LabelTranslationSlimDto>()
            {
                new LabelTranslationSlimDto() { LanguageIsoCode2 = IsoCode2One },
            };
            return dto;
        }

        public static List<LabelFatDto> GetLabelFatDtoList()
        {
            var list = new List<LabelFatDto>();
            var dto = GetLabelFatDto();
            list.Add(dto);
            var dtoOne = new LabelFatDto();
            list.Add(dtoOne);

            return list;
        }

        public static RevisionDto<LabelDto> GetRevisionDtoLabelDto()
        {
            var dto = new RevisionDto<LabelDto>();
            dto.Item = GetLabelDto();
            dto.Revision = One;
            dto.RevisionedAt = DateTimeOne;
            dto.RevisionedByName = StringOne;
            dto.RevisionedByUid = UidOne;

            return dto;
        }

        public static RevisionDto<LabelTranslationDto> GetRevisionDtoLabelTranslationDto()
        {
            var dto = new RevisionDto<LabelTranslationDto>();
            dto.Item = GetLabelTranslationDto();
            dto.Revision = One;
            dto.RevisionedAt = DateTimeOne;
            dto.RevisionedByName = StringOne;
            dto.RevisionedByUid = UidOne;

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

        public static TokenRequestLogDto GetTokenRequestLogDto()
        {
            var dto = new TokenRequestLogDto();
            dto.OrganizationUid = UidOne;
            dto.OrganizationName = StringOne;
            dto.IntegrationUid = UidTwo;
            dto.IntegrationName = StringTwo;
            dto.IntegrationClientUid = UidThree;
            dto.Uid = UidThree;
            dto.Name = StringThree;
            dto.City = StringTwo;
            dto.Country = StringThree;
            dto.HttpMethod = HttpsUrl;
            dto.ResponseCode = StringFour;
            dto.CreatedAt = DateTimeOne;

            return dto;
        }

        public static UserLoginLogDto GetUserLoginLogDto()
        {
            var dto = new UserLoginLogDto();
            dto.OrganizationUid = UidOne;
            dto.OrganizationName = StringOne;
            dto.UserUid = UidOne;
            dto.UserName = StringTwo;
            dto.Uid = UidThree;
            dto.Name = StringThree;
            dto.City = StringTwo;
            dto.Country = StringThree;
            dto.CreatedAt = DateTimeOne;
            dto.Platform = StringFive;
            dto.PlatformVersion = StringSix;
            dto.Ip = IpOne;

            return dto;
        }
    }
}