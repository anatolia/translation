using System.Collections.Generic;

using StandardRepository.Models.Entities;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using Translation.Data.Entities.Parameter;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Server.Unit.Tests.TestHelpers
{
    public class FakeEntityTestHelper
    {
        public static TranslationProvider GetTranslationProviderOne()
        {
            var model = new TranslationProvider();
            model.Id = LongOne;
            model.CredentialValue = StringOne;
            model.IsActive = BooleanTrue;
            return model;
        }

        public static TranslationProvider GetTranslationProviderTwo()
        {
            var model = new TranslationProvider();
            model.Id = LongTwo;
            model.CredentialValue = StringTwo;
            model.IsActive = BooleanTrue;
            return model;
        }

        public static TranslationProvider GetTranslationProviderNullValue()
        {
            var model = new TranslationProvider();
            model.Id = LongOne;
            model.CredentialValue = EmptyString;
            return model;
        }

        public static TranslationProvider GetTranslationProviderNotExist()
        {
            var model = GetTranslationProviderOne();
            model.Id = Zero;
            return model;
        }

        public static Language GetLanguage()
        {
            var language = new Language();

            language.Id = LongOne;
            language.Uid = UidOne;
            language.Name = StringOne;

            language.CreatedAt = DateTimeOne;

            return language;
        }

        public static Integration GetIntegration()
        {
            var integration = new Integration();
            integration.OrganizationId = LongOne;
            integration.OrganizationUid = UidOne;
            integration.OrganizationName = StringOne;

            integration.Id = LongOne;
            integration.Uid = UidOne;
            integration.Name = StringOne;

            integration.CreatedAt = DateTimeOne;
            integration.IsActive = BooleanTrue;

            return integration;
        }

        public static Integration GetIntegrationNotExist()
        {
            var integration = GetIntegration();
            integration.Id = Zero;

            return integration;
        }

        public static Integration GetOrganizationOneIntegrationOne()
        {
            var integration = new Integration();
            integration.OrganizationId = OrganizationOneId;
            integration.OrganizationUid = OrganizationOneUid;
            integration.OrganizationName = OrganizationOneName;

            integration.Id = OrganizationOneIntegrationOneId;
            integration.Uid = OrganizationOneIntegrationOneUid;
            integration.Name = OrganizationOneIntegrationOneName;
            integration.Description = OrganizationOneIntegrationOneName;
            integration.CreatedAt = DateTimeOne;
            integration.IsActive = BooleanTrue;

            return integration;
        }

        public static Integration GetOrganizationOneIntegrationOneNotActive()
        {
            var integration = GetOrganizationOneIntegrationOne();
            integration.IsActive = BooleanFalse;

            return integration;
        }

        public static Integration GetOrganizationOneIntegrationOneNotExist()
        {
            var integration = GetOrganizationOneIntegrationOne();
            integration.Id = Zero;

            return integration;
        }

        public static Integration GetOrganizationTwoIntegrationOne()
        {
            var integration = new Integration();
            integration.OrganizationId = OrganizationTwoId;
            integration.OrganizationUid = OrganizationTwoUid;
            integration.OrganizationName = OrganizationTwoName;

            integration.Id = OrganizationTwoIntegrationOneId;
            integration.Uid = OrganizationTwoIntegrationOneUid;
            integration.Name = OrganizationTwoIntegrationOneName;

            integration.CreatedAt = DateTimeOne;
            integration.IsActive = BooleanTrue;

            return integration;
        }

        public static IntegrationClient GetOrganizationOneIntegrationOneIntegrationClientOneNotExist()
        {
            var integrationClient = GetOrganizationOneIntegrationOneIntegrationClientOne();
            integrationClient.Id = Zero;

            return integrationClient;
        }

        public static IntegrationClient GetOrganizationOneIntegrationOneIntegrationClientOne()
        {
            var integrationClient = new IntegrationClient();
            integrationClient.OrganizationId = OrganizationOneId;
            integrationClient.OrganizationUid = OrganizationOneUid;
            integrationClient.OrganizationName = OrganizationOneName;

            integrationClient.IntegrationId = OrganizationOneIntegrationOneId;
            integrationClient.IntegrationUid = OrganizationOneIntegrationOneUid;
            integrationClient.IntegrationName = OrganizationOneIntegrationOneName;

            integrationClient.Id = OrganizationOneIntegrationOneIntegrationClientOneId;
            integrationClient.Uid = OrganizationOneIntegrationOneIntegrationClientOneUid;
            integrationClient.Name = OrganizationOneIntegrationOneIntegrationClientOneName;

            integrationClient.ClientId = UidOne;
            integrationClient.ClientSecret = UidTwo;
            integrationClient.CreatedAt = DateTimeOne;
            integrationClient.IsActive = BooleanTrue;

            return integrationClient;
        }

        public static IntegrationClient GetIntegrationClient()
        {
            var integrationClient = new IntegrationClient();
            integrationClient.OrganizationId = LongOne;
            integrationClient.OrganizationUid = UidOne;
            integrationClient.OrganizationName = StringOne;

            integrationClient.IntegrationId = LongOne;
            integrationClient.IntegrationUid = UidOne;
            integrationClient.IntegrationName = StringOne;

            integrationClient.ClientId = UidOne;
            integrationClient.Id = LongOne;
            integrationClient.Uid = UidOne;

            integrationClient.Name = StringOne;
            integrationClient.CreatedAt = DateTimeOne;
            integrationClient.IsActive = BooleanTrue;

            return integrationClient;
        }

        public static IntegrationClient GetOrganizationTwoIntegrationOneIntegrationClientOne()
        {
            var integrationClient = new IntegrationClient();
            integrationClient.OrganizationId = OrganizationTwoId;
            integrationClient.OrganizationUid = OrganizationTwoUid;
            integrationClient.OrganizationName = OrganizationTwoName;

            integrationClient.IntegrationId = OrganizationTwoIntegrationOneId;
            integrationClient.IntegrationUid = OrganizationTwoIntegrationOneUid;
            integrationClient.IntegrationName = OrganizationTwoIntegrationOneName;

            integrationClient.Id = OrganizationTwoIntegrationOneIntegrationClientOneId;
            integrationClient.Uid = OrganizationTwoIntegrationOneIntegrationClientOneUid;
            integrationClient.Name = OrganizationTwoIntegrationOneIntegrationClientOneName;

            integrationClient.ClientId = UidOne;
            integrationClient.ClientSecret = UidTwo;
            integrationClient.CreatedAt = DateTimeOne;
            integrationClient.IsActive = BooleanTrue;

            return integrationClient;
        }

        public static EntityRevision<Integration> GetIntegrationRevisionTwo()
        {
            var revision = new EntityRevision<Integration>();

            revision.Id = LongOne;
            revision.Entity = GetIntegration();
            revision.Revision = Two;
            revision.RevisionedAt = DateTimeOne;
            revision.RevisionedBy = LongOne;

            return revision;
        }

        public static List<EntityRevision<LabelTranslation>> GetOrganizationOneProjectOneLabelOneLabelTranslationOneRevisionsRevisionOneInIt()
        {
            var list = new List<EntityRevision<LabelTranslation>>();
            var revision = new EntityRevision<LabelTranslation>();
            revision.Id = LongOne;
            revision.Revision = One;
            revision.RevisionedAt = DateTimeOne;
            revision.Entity = GetOrganizationOneProjectOneLabelOneLabelTranslationOne();

            list.Add(revision);

            return list;
        }

        public static List<EntityRevision<LabelTranslation>> GetOrganizationOneProjectOneLabelOneLabelTranslationOneRevisionsRevisionTwoInIt()
        {
            var list = new List<EntityRevision<LabelTranslation>>();
            var revision = new EntityRevision<LabelTranslation>();
            revision.Id = LongOne;
            revision.Revision = Two;
            revision.RevisionedAt = DateTimeOne;
            revision.Entity = GetOrganizationOneProjectOneLabelOneLabelTranslationOne();

            list.Add(revision);

            return list;
        }

        public static List<EntityRevision<Integration>> GetOrganizationOneIntegrationOneRevisions()
        {
            var list = new List<EntityRevision<Integration>>();
            var revision = new EntityRevision<Integration>();
            revision.Id = LongOne;
            revision.Revision = One;
            revision.RevisionedAt = DateTimeOne;
            revision.Entity = GetOrganizationOneIntegrationOne();

            list.Add(revision);

            return list;
        }

        public static Token GetToken()
        {
            var token = new Token();
            token.OrganizationId = LongOne;
            token.OrganizationUid = UidOne;
            token.OrganizationName = StringOne;

            token.IntegrationId = LongOne;
            token.IntegrationUid = UidOne;
            token.IntegrationName = StringOne;

            token.Id = LongOne;
            token.Uid = UidOne;
            token.Name = StringOne;

            token.CreatedAt = DateTimeOne;
            token.IsActive = BooleanTrue;

            return token;
        }

        public static Token GetOrganizationOneIntegrationOneIntegrationClientOneTokenOneNotExist()
        {
            var token = GetOrganizationOneIntegrationOneIntegrationClientOneTokenOne();
            token.Id = Zero;
            return token;
        }

        public static Token GetOrganizationOneIntegrationOneIntegrationClientOneTokenOne()
        {

            var token = new Token();
            token.OrganizationId = OrganizationOneId;
            token.OrganizationUid = OrganizationOneUid;
            token.OrganizationName = OrganizationOneName;

            token.IntegrationId = OrganizationOneIntegrationOneId;
            token.IntegrationUid = OrganizationOneIntegrationOneUid;
            token.IntegrationName = OrganizationOneIntegrationOneName;

            token.Id = OrganizationOneIntegrationOneIntegrationClientOneId;
            token.Uid = OrganizationOneIntegrationOneIntegrationClientOneUid;
            token.Name = OrganizationOneIntegrationOneIntegrationClientOneName;


            token.CreatedAt = DateTimeOne;
            token.IsActive = BooleanTrue;
            token.CreatedBy = LongOne;
            return token;
        }

        public static Token GetOrganizationTwoIntegrationOneIntegrationClientOneTokenOne()
        {
            var token = new Token();
            token.OrganizationId = OrganizationTwoId;
            token.OrganizationUid = OrganizationOneUid;
            token.OrganizationName = OrganizationOneName;

            token.IntegrationId = OrganizationOneIntegrationOneId;
            token.IntegrationUid = OrganizationOneIntegrationOneUid;
            token.IntegrationName = OrganizationOneIntegrationOneName;

            token.Id = OrganizationOneIntegrationOneIntegrationClientOneId;
            token.Uid = OrganizationOneIntegrationOneIntegrationClientOneUid;
            token.Name = OrganizationOneIntegrationOneIntegrationClientOneName;


            token.CreatedAt = DateTimeOne;
            token.IsActive = BooleanTrue;

            return token;
        }

        public static TokenRequestLog GetTokenRequestLog()
        {
            var tokenRequestLog = new TokenRequestLog();
            tokenRequestLog.OrganizationId = LongOne;
            tokenRequestLog.OrganizationUid = UidOne;
            tokenRequestLog.OrganizationName = StringOne;

            tokenRequestLog.IntegrationId = LongOne;
            tokenRequestLog.IntegrationUid = UidOne;
            tokenRequestLog.IntegrationName = StringOne;

            tokenRequestLog.IntegrationClientId = LongOne;
            tokenRequestLog.IntegrationClientUid = UidOne;
            tokenRequestLog.IntegrationClientName = StringOne;

            tokenRequestLog.Id = LongOne;
            tokenRequestLog.Uid = UidOne;
            tokenRequestLog.Name = StringOne;
            tokenRequestLog.CreatedAt = DateTimeOne;

            tokenRequestLog.City = StringOne;
            tokenRequestLog.Country = StringOne;
            tokenRequestLog.Ip = IpOne;

            return tokenRequestLog;
        }

        public static Project GetProject()
        {
            var project = new Project();
            project.OrganizationId = LongOne;
            project.OrganizationUid = UidOne;
            project.OrganizationName = StringOne;

            project.LabelCount = One;
            project.LabelTranslationCount = Two;

            project.IsActive = BooleanTrue;

            project.LanguageId = LongTwo;
            project.LanguageUid = UidTwo;
            project.LanguageName = StringTwo;
            project.LanguageIconUrl = HttpsUrl;

            return project;
        }

        public static Project GetOrganizationTwoProjectOne()
        {
            var project = new Project();
            project.Id = OrganizationTwoProjectOneId;
            project.Uid = OrganizationTwoProjectOneUid;
            project.Name = OrganizationTwoProjectOneName;

            project.OrganizationId = OrganizationTwoId;
            project.OrganizationUid = OrganizationTwoUid;
            project.OrganizationName = OrganizationTwoName;

            project.IsActive = BooleanTrue;
            project.Url = HttpUrl;
            project.CreatedAt = DateTimeOne;

            project.Slug = SlugOne;
            project.LanguageUid = UidOne;

            return project;
        }

        public static Project GetOrganizationOneProjectOne()
        {
            var project = new Project();
            project.Id = OrganizationOneProjectOneId;
            project.Uid = OrganizationOneProjectOneUid;
            project.Name = OrganizationOneProjectOneName;

            project.OrganizationId = OrganizationOneId;
            project.OrganizationUid = OrganizationOneUid;
            project.OrganizationName = OrganizationOneName;

            project.Description = StringOne;
            project.IsActive = BooleanTrue;
            project.Url = HttpUrl;
            project.CreatedAt = DateTimeOne;

            project.Slug = OrganizationOneProjectOneSlug;
            project.LanguageUid = UidOne;

            return project;
        }

        public static Project GetOrganizationOneSuperProjectOne()
        {
            var project = GetOrganizationOneProjectOne();
            project.IsSuperProject = BooleanTrue;
            return project;
        }

        public static Project GetOrganizationOneSuperProjectOneNotExist()
        {
            var project = GetOrganizationOneProjectOne();
            project.IsSuperProject = BooleanTrue;
            project.Id = Zero;
            return project;
        }

        public static Project GetOrganizationOneProjectOneNotExist()
        {
            var project = GetOrganizationOneProjectOne();
            project.Id = Zero;

            return project;
        }

        public static Project GetOrganizationOneProjectOneNotActive()
        {
            var project = GetOrganizationOneProjectOne();
            project.IsActive = BooleanFalse;

            return project;
        }

        public static Organization GetOrganization()
        {
            var organization = new Organization();
            organization.Id = LongOne;
            organization.Uid = UidOne;
            organization.Name = StringOne;

            organization.CreatedAt = DateTimeOne;
            organization.Description = StringOne;
            organization.IsActive = BooleanTrue;

            organization.ObfuscationKey = StringSixtyFourOne;

            organization.IsActive = BooleanTrue;

            return organization;
        }

        public static Organization GetOrganizationOne()
        {
            var organization = new Organization();
            organization.Id = OrganizationOneId;
            organization.Uid = OrganizationOneUid;
            organization.Name = OrganizationOneName;
            organization.CreatedAt = DateTimeOne;
            organization.Description = StringOne;
            organization.IsActive = BooleanTrue;
            organization.ObfuscationKey = StringSixtyFourOne;

            organization.IsActive = BooleanTrue;

            return organization;
        }

        public static Organization GetOrganizationOneNotExist()
        {
            var organization = GetOrganizationOne();
            organization.Id = Zero;

            return organization;
        }

        public static Organization GetOrganizationTwo()
        {
            var organization = new Organization();
            organization.Id = OrganizationTwoId;
            organization.Uid = OrganizationTwoUid;
            organization.Name = OrganizationTwoName;
            organization.CreatedAt = DateTimeOne;
            organization.Description = StringOne;
            organization.IsActive = BooleanTrue;
            organization.ObfuscationKey = StringSixtyFourOne;

            organization.IsActive = BooleanTrue;

            return organization;
        }

        public static CurrentOrganization GetCurrentOrganizationOne()
        {
            var organization = new CurrentOrganization();
            organization.Id = OrganizationOneId;
            organization.Uid = OrganizationOneUid;
            organization.Name = OrganizationOneName;

            return organization;
        }

        public static UserLoginLog GetUserLoginLog()
        {
            var userLoginLog = new UserLoginLog();
            userLoginLog.OrganizationId = LongOne;
            userLoginLog.OrganizationUid = UidOne;
            userLoginLog.OrganizationName = StringOne;

            userLoginLog.Id = LongOne;
            userLoginLog.Uid = UidOne;
            userLoginLog.Name = StringOne;
            userLoginLog.CreatedAt = DateTimeOne;

            userLoginLog.City = StringOne;
            userLoginLog.Country = StringOne;
            userLoginLog.Ip = IpOne;

            return userLoginLog;
        }

        public static User GetUser()
        {
            var user = new User();
            user.OrganizationId = LongOne;
            user.OrganizationUid = UidOne;
            user.OrganizationName = StringOne;

            user.Id = LongOne;
            user.Uid = UidOne;
            user.Name = StringOne;

            user.FirstName = StringOne;
            user.LastName = StringOne;
            user.PasswordHash = PasswordHashOne;

            user.Email = EmailOne;
            user.IsActive = BooleanTrue;

            return user;
        }

        public static User GetSuperAdmin()
        {
            var user = GetUser();
            user.IsSuperAdmin = BooleanTrue;

            return user;
        }

        public static User GetOrganizationOneUserOne()
        {
            var user = new User();
            user.OrganizationId = OrganizationOneId;
            user.OrganizationUid = OrganizationOneUid;
            user.OrganizationName = OrganizationOneName;

            user.Id = OrganizationOneUserOneId;
            user.Uid = OrganizationOneUserOneUid;
            user.Name = OrganizationOneUserOneName;
            user.FirstName = OrganizationOneUserOneName;
            user.LastName = OrganizationOneUserOneName;

            user.Email = OrganizationOneUserOneEmail;
            user.IsActive = BooleanTrue;

            user.ObfuscationSalt = StringSixtyFourOne;
            user.PasswordHash = PasswordHashOne;

            user.LanguageUid = UidOne;

            return user;
        }

        public static User GetOrganizationOneUserOneInvitedAtOneDayBefore()
        {
            var user = GetOrganizationOneUserOne();
            user.InvitedAt = DateTimeOneDayBefore;

            return user;
        }

        public static User GetOrganizationOneUserOneInvitedAtOneWeekBefore()
        {
            var user = GetOrganizationOneUserOne();
            user.InvitedAt = DateTimeOneWeekBefore;

            return user;
        }

        public static User GetOrganizationOneUserOneForSuccessLogOn()
        {
            var user = GetOrganizationOneUserOne();
            user.LoginTryCount = Three;
            user.LastLoginTryAt = DateTimeTwoHoursBefore;
            user.Email = EmailOne;
            user.ObfuscationSalt = StringSixtyFourOne;
            user.PasswordHash = PasswordHashOne;

            return user;
        }

        public static User GetOrganizationOneUserOnePasswordResetRequestedAtTwoDaysBefore()
        {
            var user = GetOrganizationOneUserOne();
            user.PasswordResetRequestedAt = DateTimeTwoDaysBefore;

            return user;
        }

        public static User GetOrganizationOneUserOnePasswordResetRequestedAtFiveMinutesBefore()
        {
            var user = GetOrganizationOneUserOne();
            user.PasswordResetRequestedAt = DateTimeFiveMinutesBefore;

            return user;
        }

        public static User GetOrganizationOneUserOnePasswordResetRequestedAtOneMinuteBefore()
        {
            var user = GetOrganizationOneUserOne();
            user.PasswordResetRequestedAt = DateTimeOneMinuteBefore;

            return user;
        }

        public static User GetOrganizationOneUserOneNotExist()
        {
            var user = GetOrganizationOneUserOne();
            user.Id = Zero;

            return user;
        }

        public static User GetOrganizationOneUserOneNotActive()
        {
            var user = GetOrganizationOneUserOne();
            user.IsActive = BooleanFalse;

            return user;
        }

        public static User GetOrganizationOneSuperAdminUserOneInvitedAtOneDayBefore()
        {
            var user = GetOrganizationOneSuperAdminUserOne();
            user.InvitedAt = DateTimeOneDayBefore;

            return user;
        }

        public static User GetOrganizationOneSuperAdminUserOneInvitedAtOneWeekBefore()
        {
            var user = GetOrganizationOneSuperAdminUserOne();
            user.InvitedAt = DateTimeOneWeekBefore;

            return user;
        }

        public static User GetOrganizationOneAdminUserOne()
        {
            var user = GetOrganizationOneUserOne();
            user.IsAdmin = BooleanTrue;

            return user;
        }

        public static User GetOrganizationTwoAdminUserOne()
        {
            var user = GetOrganizationTwoUserOne();
            user.IsAdmin = BooleanTrue;

            return user;
        }

        public static User GetOrganizationOneSuperAdminUserOne()
        {
            var user = GetOrganizationOneUserOne();
            user.IsSuperAdmin = BooleanTrue;
            user.IsAdmin = BooleanTrue;
            return user;
        }

        public static User GetOrganizationTwoUserOne()
        {
            var user = new User();
            user.OrganizationId = OrganizationTwoId;
            user.OrganizationUid = OrganizationTwoUid;
            user.OrganizationName = OrganizationTwoName;

            user.Id = OrganizationTwoUserOneId;
            user.Uid = OrganizationTwoUserOneUid;
            user.Name = OrganizationTwoUserOneName;

            user.Email = OrganizationTwoUserOneEmail;
            user.IsActive = BooleanTrue;

            return user;
        }

        public static CurrentUser GetOrganizationOneCurrentUserOne()
        {
            var currentOrganization = GetCurrentOrganizationOne();
            var user = new CurrentUser();
            user.Organization = currentOrganization;
            user.Id = OrganizationOneUserOneId;
            user.Uid = OrganizationOneUserOneUid;
            user.Name = OrganizationOneUserOneName;
            user.Email = OrganizationOneUserOneEmail;
            user.IsActive = BooleanTrue;

            return user;
        }

        public static Label GetLabel()
        {
            var label = new Label();
            label.OrganizationId = LongOne;
            label.OrganizationUid = UidOne;
            label.OrganizationName = StringOne;

            label.ProjectId = LongOne;
            label.ProjectUid = UidOne;
            label.ProjectName = StringOne;

            label.Id = LongOne;
            label.Uid = UidOne;
            label.Name = StringOne;
            label.LabelKey = StringOne;

            label.IsActive = BooleanTrue;

            return label;
        }

        public static Label GetOrganizationOneProjectOneLabelOne()
        {
            var label = new Label();
            label.OrganizationId = OrganizationOneId;
            label.OrganizationUid = OrganizationOneUid;
            label.OrganizationName = OrganizationOneName;

            label.ProjectId = OrganizationOneProjectOneId;
            label.ProjectUid = OrganizationOneProjectOneUid;
            label.ProjectName = OrganizationOneProjectOneName;

            label.Id = OrganizationOneProjectOneLabelOneId;
            label.Uid = OrganizationOneProjectOneLabelOneUid;
            label.Name = OrganizationOneProjectOneLabelOneName;
            label.LabelKey = OrganizationOneProjectOneLabelOneKey;
            label.Description = StringOne;
            label.IsActive = BooleanTrue;

            return label;
        }

        public static Label GetOrganizationTwoProjectOneLabelOne()
        {
            var label = new Label();
            label.OrganizationId = OrganizationTwoId;
            label.OrganizationUid = OrganizationTwoUid;
            label.OrganizationName = OrganizationTwoName;

            label.ProjectId = OrganizationTwoProjectOneId;
            label.ProjectUid = OrganizationTwoProjectOneUid;
            label.ProjectName = OrganizationTwoProjectOneName;

            label.Id = OrganizationTwoProjectOneLabelOneId;
            label.Uid = OrganizationTwoProjectOneLabelOneUid;
            label.Name = OrganizationTwoProjectOneLabelOneName;
            label.LabelKey = OrganizationTwoProjectOneLabelOneKey;

            label.IsActive = BooleanTrue;

            return label;
        }

        public static Label GetOrganizationOneProjectOneLabelOneNotExist()
        {
            var label = GetOrganizationOneProjectOneLabelOne();
            label.Id = Zero;

            return label;
        }

        public static Label GetOrganizationOneProjectOneLabelOneNotActive()
        {
            var label = GetOrganizationOneProjectOneLabelOne();
            label.IsActive = BooleanFalse;

            return label;
        }

        public static LabelTranslation GetLabelTranslation()
        {
            var labelTranslation = new LabelTranslation();
            labelTranslation.OrganizationId = LongOne;
            labelTranslation.OrganizationUid = UidOne;
            labelTranslation.OrganizationName = StringOne;

            labelTranslation.ProjectId = LongOne;
            labelTranslation.ProjectUid = UidOne;
            labelTranslation.ProjectName = StringOne;

            labelTranslation.LabelId = LongOne;
            labelTranslation.LabelUid = UidOne;
            labelTranslation.LabelName = StringOne;

            labelTranslation.Id = LongOne;
            labelTranslation.Uid = UidOne;
            labelTranslation.Name = StringOne;
            labelTranslation.TranslationText = StringOne;
            labelTranslation.IsActive = BooleanTrue;
            labelTranslation.LanguageId = LongOne;
            labelTranslation.LanguageName = StringOne;

            return labelTranslation;
        }

        public static List<LabelTranslation> GetLabelTranslationList()
        {
            var list = new List<LabelTranslation>();
            list.Add(GetLabelTranslation());
            list.Add(GetOrganizationOneProjectOneLabelOneLabelTranslationOne());

            return list;
        }

        public static LabelTranslation GetOrganizationOneProjectOneLabelOneLabelTranslationOne()
        {
            var labelTranslation = new LabelTranslation();
            labelTranslation.OrganizationId = OrganizationOneId;
            labelTranslation.OrganizationUid = OrganizationOneUid;
            labelTranslation.OrganizationName = OrganizationOneName;

            labelTranslation.ProjectId = OrganizationOneProjectOneId;
            labelTranslation.ProjectUid = OrganizationOneProjectOneUid;
            labelTranslation.ProjectName = OrganizationOneProjectOneName;

            labelTranslation.LabelId = OrganizationOneProjectOneLabelOneId;
            labelTranslation.LabelUid = OrganizationOneProjectOneLabelOneUid;
            labelTranslation.LabelName = OrganizationOneProjectOneLabelOneName;

            labelTranslation.Id = OrganizationOneProjectOneLabelOneLabelTranslationOneId;
            labelTranslation.Uid = UidTwo;
            labelTranslation.Name = OrganizationOneProjectOneLabelOneLabelTranslationOneName;
            labelTranslation.TranslationText = OrganizationOneProjectOneLabelOneLabelTranslationOneName;
            labelTranslation.Description = StringOne;
            labelTranslation.IsActive = BooleanTrue;

            return labelTranslation;
        }

        public static LabelTranslation GetOrganizationTwoProjectOneLabelOneLabelTranslationOne()
        {
            var labelTranslation = new LabelTranslation();
            labelTranslation.OrganizationId = OrganizationTwoId;
            labelTranslation.OrganizationUid = OrganizationTwoUid;
            labelTranslation.OrganizationName = OrganizationTwoName;

            labelTranslation.ProjectId = OrganizationTwoProjectOneId;
            labelTranslation.ProjectUid = OrganizationTwoProjectOneUid;
            labelTranslation.ProjectName = OrganizationTwoProjectOneName;

            labelTranslation.LabelId = OrganizationTwoProjectOneLabelOneId;
            labelTranslation.LabelUid = OrganizationTwoProjectOneLabelOneUid;
            labelTranslation.LabelName = OrganizationTwoProjectOneLabelOneName;

            labelTranslation.Id = OrganizationTwoProjectOneLabelOneId;
            labelTranslation.Uid = OrganizationTwoProjectOneLabelOneUid;
            labelTranslation.Name = OrganizationTwoProjectOneLabelOneName;

            labelTranslation.IsActive = BooleanTrue;

            return labelTranslation;
        }

        public static LabelTranslation GetOrganizationOneProjectOneLabelOneLabelTranslationOneNotExist()
        {
            var labelTranslation = GetOrganizationOneProjectOneLabelOneLabelTranslationOne();
            labelTranslation.Id = Zero;

            return labelTranslation;
        }

        public static LabelTranslation GetOrganizationOneProjectOneLabelOneLabelTranslationOneNotActive()
        {
            var labelTranslation = GetOrganizationOneProjectOneLabelOneLabelTranslationOne();
            labelTranslation.IsActive = BooleanFalse;

            return labelTranslation;
        }

        public static Language GetLanguageOneNotExist()
        {
            var language = GetLanguageOne();
            language.Id = Zero;

            return language;
        }

        public static Language GetLanguageOne()
        {
            var language = new Language();
            language.Name = LanguageOne;
            language.Id = LongOne;
            language.Uid = UidOne;

            language.OriginalName = LanguageOneOriginalName;
            language.IsoCode2Char = IsoCode2One;
            language.IsoCode3Char = IsoCode3One;
            language.IconUrl = StringOne;
            return language;
        }

        public static List<Language> GetLanguageOneList()
        {
            var list = new List<Language>();
            list.Add(GetLanguageOne());
            list.Add(GetLanguageTwo());

            return list;
        }

        public static Language GetLanguageTwo()
        {
            var language = new Language();
            language.Name = LanguageTwo;
            language.Id = LongTwo;
            language.Uid = UidTwo;

            language.OriginalName = LanguageTwoOriginalName;
            language.IsoCode2Char = IsoCode2Two;
            language.IsoCode3Char = IsoCode3Two;
            language.IconUrl = StringTwo;
            language.Description = StringTwo;

            return language;
        }

        public static EntityRevision<Language> GetLanguageRevisionOne()
        {
            var revision = new EntityRevision<Language>();

            revision.Id = LongOne;
            revision.Entity = GetLanguageOne();
            revision.Revision = One;
            revision.RevisionedAt = DateTimeOne;
            revision.RevisionedBy = LongOne;

            return revision;
        }

        public static EntityRevision<Language> GetLanguageRevisionTwo()
        {
            var revision = GetLanguageRevisionOne();
            revision.Revision = Two;

            return revision;
        }

        public static List<EntityRevision<Organization>> GetOrganizationOneRevisionsRevisionOneInIt()
        {
            var list = new List<EntityRevision<Organization>>();
            var revision = new EntityRevision<Organization>();
            revision.Id = LongOne;
            revision.Revision = One;
            revision.RevisionedAt = DateTimeOne;
            revision.Entity = GetOrganizationOne();

            list.Add(revision);

            return list;
        }

        public static List<EntityRevision<Organization>> GetOrganizationOneRevisionsRevisionTwoInIt()
        {
            var list = new List<EntityRevision<Organization>>();
            var revision = new EntityRevision<Organization>();
            revision.Id = LongOne;
            revision.Revision = Two;
            revision.RevisionedAt = DateTimeOne;
            revision.Entity = GetOrganizationOne();

            list.Add(revision);

            return list;
        }

        public static List<EntityRevision<User>> GetOrganizationOneUserOneRevisions()
        {
            var list = new List<EntityRevision<User>>();
            var revision = new EntityRevision<User>();
            revision.Id = LongOne;
            revision.Revision = One;
            revision.RevisionedAt = DateTimeOne;
            revision.Entity = GetOrganizationOneUserOne();

            list.Add(revision);

            return list;
        }

        public static List<EntityRevision<User>> GetOrganizationOneUserOneRevisionsRevisionTwoInIt()
        {
            var list = new List<EntityRevision<User>>();
            var revision = new EntityRevision<User>();
            revision.Id = LongOne;
            revision.Revision = Two;
            revision.RevisionedAt = DateTimeOne;
            revision.Entity = GetOrganizationOneUserOne();

            list.Add(revision);

            return list;
        }

        public static List<EntityRevision<Project>> GetOrganizationOneProjectOneRevisions()
        {
            var list = new List<EntityRevision<Project>>();
            var revision = new EntityRevision<Project>();
            revision.Id = LongOne;
            revision.Revision = One;
            revision.RevisionedAt = DateTimeOne;
            revision.Entity = GetOrganizationOneProjectOne();

            list.Add(revision);

            return list;
        }

        public static List<EntityRevision<Label>> GetOrganizationOneProjectOneLabelOneRevisionsRevisionOneInIt()
        {
            var list = new List<EntityRevision<Label>>();
            var revision = new EntityRevision<Label>();
            revision.Id = LongOne;
            revision.Revision = One;
            revision.RevisionedAt = DateTimeOne;
            revision.Entity = GetOrganizationOneProjectOneLabelOne();

            list.Add(revision);

            return list;
        }

        public static Journal GetJournal()
        {
            var journal = new Journal();
            journal.OrganizationId = LongOne;
            journal.OrganizationUid = UidOne;
            journal.OrganizationName = StringOne;

            journal.IntegrationId = LongOne;
            journal.IntegrationUid = UidOne;
            journal.IntegrationName = StringOne;

            journal.UserId = LongOne;
            journal.UserUid = UidOne;
            journal.UserName = StringOne;

            journal.Message = StringOne;

            return journal;
        }

        public static SendEmailLog GetSendEmailLog()
        {
            var sendEmailLog = new SendEmailLog();
            sendEmailLog.OrganizationId = LongOne;
            sendEmailLog.OrganizationUid = UidOne;
            sendEmailLog.OrganizationName = StringOne;

            sendEmailLog.Id = LongOne;
            sendEmailLog.Uid = UidOne;
            sendEmailLog.Name = StringOne;
            sendEmailLog.CreatedAt = DateTimeOne;

            sendEmailLog.EmailFrom = StringOne;
            sendEmailLog.EmailTo = StringOne;
            sendEmailLog.Subject = StringOne;

            return sendEmailLog;
        }

        public static (bool, Organization, User) GetTrueOrganizationUser()
        {
            var uowResult = BooleanTrue;
            var organization = GetOrganization();
            var user = GetUser();

            return (uowResult, organization, user);
        }

        public static (bool, Organization, User) GetFalseOrganizationUser()
        {
            var uowResult = BooleanFalse;
            var organization = GetOrganization();
            var user = GetUser();

            return (uowResult, organization, user);
        }
    }
}