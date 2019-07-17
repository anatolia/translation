using System.Collections.Generic;

using StandardRepository.Models.Entities;

using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using Translation.Data.Entities.Parameter;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.TestHelpers
{
    public class FakeEntityTestHelper
    {
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

        public static Project GetOrganizationOneProjectOne()
        {
            var project = new Project();
            project.Id = OrganizationOneProjectOneId;
            project.Uid = OrganizationOneProjectOneUid;
            project.Name = OrganizationOneProjectOneName;
            project.OrganizationId = OrganizationOneId;
            project.OrganizationUid = OrganizationOneUid;
            project.OrganizationName = OrganizationOneName;
            project.IsActive = BooleanTrue;
            project.Url = HttpUrl;
            project.CreatedAt = DateTimeOne;

            return project;
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

            user.Email = OrganizationOneUserOneEmail;
            user.IsActive = BooleanTrue;

            return user;
        }

        public static User GetOrganizationOneAdminUserOne()
        {
            var user = GetOrganizationOneUserOne();
            user.IsAdmin = BooleanTrue;

            return user;
        }

        public static User GetOrganizationOneSuperAdminUserOne()
        {
            var user = GetOrganizationOneUserOne();
            user.IsSuperAdmin = BooleanTrue;

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

            return organization;
        }

        public static CurrentOrganization GetCurrentOrganizationTwo()
        {
            var organization = new CurrentOrganization();
            organization.Id = OrganizationTwoId;
            organization.Uid = OrganizationTwoUid;
            organization.Name = OrganizationTwoName;

            return organization;
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

            return project;
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

        public static CurrentUser GetOrganizationTwoCurrentUserOne()
        {
            var user = new CurrentUser();
            user.Id = OrganizationTwoUserOneId;
            user.Uid = OrganizationTwoUserOneUid;
            user.Name = OrganizationTwoUserOneName;
            user.Email = OrganizationTwoUserOneEmail;
            user.IsActive = BooleanTrue;

            return user;
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
            label.Key = OrganizationOneProjectOneLabelOneKey;

            return label;
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

            labelTranslation.Id = OrganizationOneProjectOneLabelOneId;
            labelTranslation.Uid = OrganizationOneProjectOneLabelOneUid;
            labelTranslation.Name = OrganizationOneProjectOneLabelOneName;

            return labelTranslation;
        }

        public static Language GetLanguageOne()
        {
            var language = new Language();
            language.IsoCode2Char = IsoCode2One;
            language.IsoCode3Char = IsoCode3One;

            return language;
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
    }
}