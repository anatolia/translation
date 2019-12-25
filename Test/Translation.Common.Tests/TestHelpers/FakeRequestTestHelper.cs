using System;
using System.Collections.Generic;
using System.Net;

using StandardUtils.Models.Shared;

using Translation.Common.Models.Requests.Admin;
using Translation.Common.Models.Requests.Integration;
using Translation.Common.Models.Requests.Integration.IntegrationClient;
using Translation.Common.Models.Requests.Integration.Token;
using Translation.Common.Models.Requests.Journal;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Requests.Label.LabelTranslation;
using Translation.Common.Models.Requests.Language;
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Requests.Project;
using Translation.Common.Models.Requests.SendEmailLog;
using Translation.Common.Models.Requests.TranslationProvider;
using Translation.Common.Models.Requests.User;
using Translation.Common.Models.Requests.User.LoginLog;
using Translation.Common.Models.Responses.Admin;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using Translation.Data.Entities.Parameter;

using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.TestHelpers
{
    public class FakeRequestTestHelper
    {
        public static AllActiveTokenReadListRequest GetAllActiveTokenReadListRequest()
        {
            var request = new AllActiveTokenReadListRequest(CurrentUserId);

            return request;
        }

        public static IntegrationClientTokenRequestLogReadListRequest GetIntegrationClientTokenRequestLogReadListRequest()
        {
            var request = new IntegrationClientTokenRequestLogReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static IntegrationTokenRequestLogReadListRequest GetIntegrationTokenRequestLogReadListRequest()
        {
            var request = new IntegrationTokenRequestLogReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static OrganizationTokenRequestLogReadListRequest GetOrganizationTokenRequestLogReadListRequest()
        {
            var request = new OrganizationTokenRequestLogReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static OrganizationTokenRequestLogReadListRequest GetOrganizationTokenRequestLogReadListRequest(long currentUserId, Guid organizationUid)
        {
            var request = new OrganizationTokenRequestLogReadListRequest(currentUserId, organizationUid);

            return request;
        }

        public static OrganizationActiveTokenReadListRequest GetOrganizationActiveTokenReadListRequest()
        {
            var request = new OrganizationActiveTokenReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static OrganizationActiveTokenReadListRequest GetOrganizationActiveTokenReadListRequest(long currentUserId, Guid organizationUid)
        {
            var request = new OrganizationActiveTokenReadListRequest(currentUserId, organizationUid);

            return request;
        }

        public static IntegrationClientActiveTokenReadListRequest GetIntegrationClientActiveTokenReadListRequest()
        {
            var request = new IntegrationClientActiveTokenReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static IntegrationActiveTokenReadListRequest GetIntegrationActiveTokenReadListRequest()
        {
            var request = new IntegrationActiveTokenReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static IntegrationActiveTokenReadListRequest GetIntegrationActiveTokenReadListRequest(long currentUserId, Guid integrationUid)
        {
            var request = new IntegrationActiveTokenReadListRequest(currentUserId, integrationUid);

            return request;
        }

        public static TokenCreateRequest GetTokenCreateRequest()
        {
            var request = new TokenCreateRequest(UidOne, UidOne, IPAddress.Any);

            return request;
        }

        public static TokenCreateRequest GetTokenCreateRequest(Guid clientId, Guid clientSecret, IPAddress ip)
        {
            var request = new TokenCreateRequest(clientId, clientSecret, ip);

            return request;
        }

        public static TokenCreateRequest GetTokenCreateRequest(IntegrationClient integrationClient)
        {
            var request = new TokenCreateRequest(integrationClient.ClientId, integrationClient.ClientId, IPAddress.Any);

            return request;
        }

        public static TokenGetRequest GetTokenGetRequest()
        {
            var request = new TokenGetRequest(CurrentUserId);

            return request;
        }

        public static TokenRevokeRequest GetTokenRevokeRequest()
        {
            var request = new TokenRevokeRequest(CurrentUserId, UidOne, UidOne);

            return request;
        }

        public static TokenRevokeRequest GetTokenRevokeRequest(long currentUserId, Guid token, Guid integrationClientUid)
        {
            var request = new TokenRevokeRequest(currentUserId, token, integrationClientUid);

            return request;
        }

        public static TokenValidateRequest GetTokenValidateRequest()
        {
            var request = new TokenValidateRequest(UidOne, UidOne);

            return request;
        }

        public static TokenValidateRequest GetTokenValidateRequest(Guid projectUid, Guid token)
        {
            var request = new TokenValidateRequest(projectUid, token);

            return request;
        }

        public static JournalCreateRequest GetJournalCreateRequest()
        {
            var request = new JournalCreateRequest(CurrentUserId, StringOne);

            return request;
        }

        public static JournalCreateRequest GetJournalCreateRequest(long currentUserId, string message)
        {
            var request = new JournalCreateRequest(currentUserId, message);

            return request;
        }

        public static JournalCreateRequest GetJournalCreateRequest(Journal journal, CurrentUser currentUser)
        {
            var request = new JournalCreateRequest(CurrentUserId, journal.Message);

            return request;
        }

        public static IntegrationClientChangeActivationRequest GetIntegrationClientChangeActivationRequest()
        {
            var request = new IntegrationClientChangeActivationRequest(CurrentUserId, OrganizationOneIntegrationOneIntegrationClientOneUid);

            return request;
        }

        public static IntegrationClientChangeActivationRequest GetIntegrationClientChangeActivationRequest(long currentUserId, Guid integrationClientUid)
        {
            var request = new IntegrationClientChangeActivationRequest(currentUserId, integrationClientUid);

            return request;
        }

        public static IntegrationClientDeleteRequest GetIntegrationClientDeleteRequest()
        {
            var request = new IntegrationClientDeleteRequest(CurrentUserId, OrganizationOneIntegrationOneIntegrationClientOneUid);

            return request;
        }

        public static IntegrationClientRefreshRequest GetIntegrationClientRefreshRequest()
        {
            var request = new IntegrationClientRefreshRequest(CurrentUserId, OrganizationOneIntegrationOneIntegrationClientOneUid);

            return request;
        }

        public static IntegrationClientReadListRequest GetIntegrationClientReadListRequest(long currentUserId, Guid integrationUid)
        {
            var request = new IntegrationClientReadListRequest(currentUserId, integrationUid);

            return request;
        }

        public static IntegrationClientReadListRequest GetIntegrationClientReadListRequest()
        {
            var request = new IntegrationClientReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static IntegrationClientReadRequest GetIntegrationClientReadRequest()
        {

            var request = new IntegrationClientReadRequest(CurrentUserId, UidOne);

            return request;
        }

        public static IntegrationClientReadRequest GetIntegrationClientReadRequest(long currentUserId, Guid integrationClientUid)
        {

            var request = new IntegrationClientReadRequest(currentUserId, integrationClientUid);

            return request;
        }

        public static IntegrationClientCreateRequest GetIntegrationClientCreateRequest()
        {

            var request = new IntegrationClientCreateRequest(CurrentUserId, UidOne);

            return request;
        }

        public static IntegrationClientCreateRequest GetIntegrationClientCreateRequest(long currentUserId, Guid integrationUid)
        {

            var request = new IntegrationClientCreateRequest(currentUserId, integrationUid);

            return request;
        }

        public static IntegrationClientEditRequest GetIntegrationClientEditRequest(long currentUserId, Guid integrationClientUid, string name,
                                                                                   string description)
        {

            var request = new IntegrationClientEditRequest(currentUserId, integrationClientUid, name, description);

            return request;
        }

        public static IntegrationRestoreRequest GetIntegrationRestoreRequest()
        {
            var request = new IntegrationRestoreRequest(CurrentUserId, UidOne, One);

            return request;
        }

        public static IntegrationRestoreRequest GetIntegrationRestoreRequest(long currentUserId, Guid integrationUid, int revision)
        {
            var request = new IntegrationRestoreRequest(currentUserId, integrationUid, revision);

            return request;
        }

        public static IntegrationChangeActivationRequest GetIntegrationChangeActivationRequest(long currentUserId, Guid integrationUid)
        {
            var request = new IntegrationChangeActivationRequest(currentUserId, integrationUid);

            return request;
        }

        public static IntegrationChangeActivationRequest GetIntegrationChangeActivationRequest()
        {
            var request = new IntegrationChangeActivationRequest(CurrentUserId, UidOne);

            return request;
        }

        public static IntegrationCreateRequest GetIntegrationCreateRequest(long currentUserId, Guid organizationUid, string name,
                                                                           string description)
        {
            var request = new IntegrationCreateRequest(currentUserId, organizationUid, name,
                description);

            return request;
        }

        public static IntegrationCreateRequest GetIntegrationCreateRequest()
        {
            var request = new IntegrationCreateRequest(CurrentUserId, OrganizationOneUid, StringOne,
                StringOne);

            return request;
        }

        public static IntegrationCreateRequest GetIntegrationCreateRequest(Integration integration)
        {
            var request = new IntegrationCreateRequest(CurrentUserId, integration.OrganizationUid, integration.Name,
                integration.Description);

            return request;
        }

        public static IntegrationCreateRequest GetIntegrationCreateRequest(Integration integration, Organization organization)
        {
            var request = new IntegrationCreateRequest(CurrentUserId, organization.Uid, integration.Name,
                integration.Description);

            return request;
        }

        public static IntegrationCreateRequest GetIntegrationCreateRequest(Integration integration, CurrentOrganization currentOrganization)
        {
            var request = new IntegrationCreateRequest(CurrentUserId, currentOrganization.Uid, integration.Name,
                integration.Description);

            return request;
        }

        public static IntegrationReadListRequest GetIntegrationReadListRequest()
        {
            var request = new IntegrationReadListRequest(CurrentUserId);
            return request;
        }

        public static IntegrationReadListRequest GetIntegrationReadListRequest(long currentUserId, Guid organizationUid)
        {
            var request = new IntegrationReadListRequest(CurrentUserId);

            return request;
        }

        public static IntegrationBaseRequest GetIntegrationBaseRequest(long currentUserId, Guid integrationUid)
        {
            var request = new IntegrationBaseRequest(currentUserId, integrationUid);

            return request;
        }

        public static IntegrationClientReadListRequest GetIntegrationClientReadListRequestForSelectAfter()
        {
            var request = GetIntegrationClientReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static IntegrationClientReadListRequest GetIntegrationClientReadListRequestForSelectMany()
        {
            var request = GetIntegrationClientReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static IntegrationReadListRequest GetIntegrationReadListRequestForSelectAfter()
        {
            var request = GetIntegrationReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);


            return request;
        }

        public static IntegrationReadListRequest GetIntegrationReadListRequestForSelectMany()
        {
            var request = GetIntegrationReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static IntegrationClientBaseRequest GetIntegrationClientBaseRequest(long currentUserId, Guid integrationClientUid)
        {
            var request = new IntegrationClientBaseRequest(currentUserId, integrationClientUid);

            return request;
        }

        public static IntegrationRevisionReadListRequest GetIntegrationRevisionReadListRequest()
        {
            var request = new IntegrationRevisionReadListRequest(CurrentUserId, OrganizationOneIntegrationOneUid);

            return request;
        }

        public static IntegrationRevisionReadListRequest GetIntegrationRevisionReadListRequest(long currentUserId, Guid integrationUid)
        {
            var request = new IntegrationRevisionReadListRequest(currentUserId, integrationUid);

            return request;
        }

        public static IntegrationReadRequest GetIntegrationReadRequest()
        {
            var request = new IntegrationReadRequest(CurrentUserId, OrganizationOneIntegrationOneUid);

            return request;
        }

        public static IntegrationReadRequest GetIntegrationReadRequest(long currentUserId, Guid integrationUid)
        {
            var request = new IntegrationReadRequest(currentUserId, integrationUid);

            return request;
        }

        public static IntegrationEditRequest GetIntegrationEditRequest()
        {
            var request = new IntegrationEditRequest(CurrentUserId, OrganizationOneIntegrationOneUid, StringOne,
                                                    StringOne);

            return request;
        }

        public static IntegrationEditRequest GetSameNameAndDescriptionIntegrationEditRequest()
        {
            var request = new IntegrationEditRequest(CurrentUserId, OrganizationOneIntegrationOneUid, OrganizationOneIntegrationOneName,
                                                     OrganizationOneIntegrationOneName);

            return request;
        }

        public static IntegrationEditRequest GetIntegrationEditRequest(long currentUserId, Guid integrationUid, string name,
                                                                       string description)
        {
            var request = new IntegrationEditRequest(currentUserId, integrationUid, name,
                                                     description);

            return request;
        }

        public static IntegrationEditRequest GetIntegrationEditRequest(Integration integration)
        {
            var request = new IntegrationEditRequest(CurrentUserId, OrganizationOneIntegrationOneUid, StringOne,
                                                     StringOne);

            return request;
        }

        public static IntegrationDeleteRequest GetIntegrationDeleteRequest()
        {
            var request = new IntegrationDeleteRequest(CurrentUserId, OrganizationOneIntegrationOneUid);

            return request;
        }

        public static IntegrationDeleteRequest GetIntegrationDeleteRequest(long currentUserId, Guid integrationUid)
        {
            var request = new IntegrationDeleteRequest(currentUserId, integrationUid);

            return request;
        }

        public static ProjectCreateRequest GetProjectCreateRequest()
        {
            var request = new ProjectCreateRequest(CurrentUserId, OrganizationOneUid, StringOne,
                                                   HttpUrl, StringOne, StringOne, UidOne);

            return request;
        }

        public static ProjectCreateRequest GetProjectCreateRequest(long currentUserId, Guid organizationUid, string projectName,
                                                                   string url, string description, string projectSlug,
                                                                   Guid languageUid)
        {
            var request = new ProjectCreateRequest(currentUserId, organizationUid, projectName,
                                                   url, description, projectSlug, languageUid);

            return request;
        }

        public static ProjectCreateRequest GetProjectCreateRequest(Organization organization, Project project)
        {
            var request = new ProjectCreateRequest(CurrentUserId, organization.Uid, project.Name,
                                                   project.Url, project.Description, project.Slug,
                                                   project.LanguageUid);

            return request;
        }

        public static ProjectCreateRequest GetProjectCreateRequest(CurrentOrganization organization, Project project)
        {
            var request = new ProjectCreateRequest(CurrentUserId, organization.Uid, project.Name,
                                                   project.Url, project.Description, project.Slug,
                                                   project.LanguageUid);

            return request;
        }

        public static ProjectEditRequest GetProjectEditRequest()
        {
            var request = new ProjectEditRequest(CurrentUserId, OrganizationOneUid, UidOne,
                                                 StringOne, HttpUrl, StringOne,
                                                 StringOne, UidOne);

            return request;
        }

        public static ProjectEditRequest GetNotDifferentProjectEditRequest()
        {
            var request = new ProjectEditRequest(CurrentUserId, OrganizationOneUid, OrganizationOneProjectOneUid,
                                                 OrganizationOneProjectOneName, HttpUrl, StringOne,
                                                 OrganizationOneProjectOneSlug, UidOne);

            return request;
        }

        public static ProjectEditRequest GetProjectEditRequest(long currentUserId, Guid organizationUid, Guid projectUid,
                                                                string projectName, string url, string description,
                                                                string projectSlug, Guid languageUid)
        {
            var request = new ProjectEditRequest(currentUserId, organizationUid, projectUid,
                                                 projectName, url, description,
                                                 projectSlug, languageUid);

            return request;
        }

        public static ProjectEditRequest GetProjectEditRequest(Project project)
        {
            var request = new ProjectEditRequest(CurrentUserId, project.OrganizationUid, project.Uid,
                                                 project.Name, project.Url, project.Description,
                                                 project.Slug, project.LanguageUid);

            return request;
        }

        public static ProjectCloneRequest GetProjectCloneRequest()
        {
            var request = new ProjectCloneRequest(CurrentUserId, OrganizationOneUid, UidOne,
                                                  StringOne, HttpUrl, StringOne,
                                                  One, Two, BooleanTrue,
                                                  StringOne, UidOne);

            return request;
        }

        public static ProjectCloneRequest GetProjectCloneRequest(long currentUserId, Guid organizationUid, Guid cloningProjectUid,
                                                                 string name, string url, string description,
                                                                 int labelCount, int labelTranslationCount, bool isSuperProject,
                                                                 string slug, Guid languageUid)
        {
            var request = new ProjectCloneRequest(currentUserId, organizationUid, cloningProjectUid,
                                                  name, url, description,
                                                  labelCount, labelTranslationCount, isSuperProject,
                                                  slug, languageUid);

            return request;
        }

        public static ProjectCloneRequest GetProjectCloneRequest(Project project)
        {
            var request = new ProjectCloneRequest(CurrentUserId, project.OrganizationUid, project.Uid,
                                                  project.Name, project.Url, project.Description,
                                                  project.LabelCount, project.LabelTranslationCount, project.IsSuperProject,
                                                  project.Slug, project.LanguageUid);

            return request;
        }

        public static ProjectReadListRequest GetProjectReadListRequest()
        {
            var request = new ProjectReadListRequest(CurrentUserId);

            return request;
        }

        public static ProjectReadListRequest GetProjectReadListRequest(long currentUserId, Guid organizationUi)
        {
            var request = new ProjectReadListRequest(currentUserId);

            return request;
        }

        public static ProjectReadListRequest GetProjectReadListRequestForSelectAfter()
        {
            var request = GetProjectReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);


            return request;
        }

        public static ProjectReadListRequest GetProjectReadListRequestForSelectMany()
        {
            var request = GetProjectReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);


            return request;
        }

        public static ProjectReadRequest GetProjectReadRequest()
        {
            var request = new ProjectReadRequest(CurrentUserId, OrganizationOneProjectOneUid);

            return request;
        }

        public static ProjectReadRequest GetProjectReadRequest(long currentUserId, Guid projectUid)
        {
            var request = new ProjectReadRequest(currentUserId, projectUid);

            return request;
        }

        public static ProjectReadBySlugRequest GetProjectReadBySlugRequest()
        {
            var request = new ProjectReadBySlugRequest(CurrentUserId, StringOne);

            return request;
        }

        public static ProjectReadBySlugRequest GetProjectReadBySlugRequest(long currentUserId, string projectSlug)
        {
            var request = new ProjectReadBySlugRequest(currentUserId, projectSlug);

            return request;
        }

        public static ProjectDeleteRequest GetProjectDeleteRequest()
        {
            var request = new ProjectDeleteRequest(CurrentUserId, OrganizationOneProjectOneUid);

            return request;
        }

        public static ProjectDeleteRequest GetProjectDeleteRequest(long currentUserId, Guid projectUid)
        {
            var request = new ProjectDeleteRequest(currentUserId, projectUid);

            return request;
        }

        public static ProjectChangeActivationRequest GetProjectChangeActivationRequest()
        {
            var request = new ProjectChangeActivationRequest(CurrentUserId, OrganizationOneUid, OrganizationOneProjectOneUid);

            return request;
        }

        public static ProjectChangeActivationRequest GetProjectChangeActivationRequest(long currentUserId, Guid organizationUid, Guid projectUid)
        {
            var request = new ProjectChangeActivationRequest(currentUserId, organizationUid, projectUid);

            return request;
        }

        public static UserChangeActivationRequest GetUserChangeActivationRequest()
        {
            var request = new UserChangeActivationRequest(CurrentUserId, UidOne);

            return request;
        }

        public static AdminDemoteRequest GetAdminDemoteRequest()
        {
            var request = new AdminDemoteRequest(CurrentUserId, UidOne);

            return request;
        }

        public static AdminUpgradeRequest GetAdminUpgradeRequest()
        {
            var request = new AdminUpgradeRequest(CurrentUserId, UidOne);

            return request;
        }

        public static OrganizationChangeActivationRequest GetOrganizationChangeActivationRequest()
        {
            var request = new OrganizationChangeActivationRequest(CurrentUserId, UidOne);

            return request;
        }

        public static OrganizationChangeActivationRequest GetOrganizationChangeActivationRequest(long currentUserId, Guid organizationUid)
        {
            var request = new OrganizationChangeActivationRequest(currentUserId, organizationUid);

            return request;
        }

        public static ProjectRestoreRequest GetProjectRestoreRequest()
        {
            var request = new ProjectRestoreRequest(CurrentUserId, OrganizationOneUid, One);

            return request;
        }

        public static ProjectRestoreRequest GetProjectRestoreRequest(long currentUserId, Guid projectUid, int revision)
        {
            var request = new ProjectRestoreRequest(currentUserId, projectUid, revision);

            return request;
        }

        public static ProjectRevisionReadListRequest GetProjectRevisionReadListRequest()
        {
            var request = new ProjectRevisionReadListRequest(CurrentUserId, OrganizationOneProjectOneUid);

            return request;
        }

        public static ProjectRevisionReadListRequest GetProjectRevisionReadListRequest(long currentUserId, Guid projectUid)
        {
            var request = new ProjectRevisionReadListRequest(currentUserId, projectUid);

            return request;
        }

        public static ProjectPendingTranslationReadListRequest GetProjectPendingTranslationReadListRequest()
        {
            var request = new ProjectPendingTranslationReadListRequest(CurrentUserId, OrganizationOneProjectOneUid);

            return request;
        }

        public static ProjectPendingTranslationReadListRequest GetProjectPendingTranslationReadListRequest(long currentUserId, Guid projectUid)
        {
            var request = new ProjectPendingTranslationReadListRequest(currentUserId, projectUid);

            return request;
        }

        public static ProjectPendingTranslationReadListRequest GetProjectPendingTranslationReadListRequestForSelectAfter()
        {
            var request = GetProjectPendingTranslationReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static ProjectPendingTranslationReadListRequest GetProjectPendingTranslationReadListRequestForSelectMany()
        {
            var request = GetProjectPendingTranslationReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static TranslationProviderReadRequest GetTranslationProviderReadRequest()
        {
            var request = new TranslationProviderReadRequest(CurrentUserId, UidOne);

            return request;
        }

        public static TranslationProviderReadListRequest GetTranslationProviderReadListRequest()
        {
            var request = new TranslationProviderReadListRequest();

            return request;
        }

        public static TranslationProviderReadListRequest GetTranslationProviderReadListRequestForSelectAfter()
        {
            var request = new TranslationProviderReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static TranslationProviderReadListRequest GetTranslationProviderReadListRequestForSelectMany()
        {
            var request = new TranslationProviderReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static TranslationProviderChangeActivationRequest GetTranslationProviderChangeActivationRequest()
        {
            var request = new TranslationProviderChangeActivationRequest(CurrentUserId, UidOne);

            return request;
        }

        public static TranslationProviderEditRequest GetTranslationProviderEditRequest()
        {
            var request = new TranslationProviderEditRequest(CurrentUserId, UidOne, StringTwo, StringThree);

            return request;
        }

        public static TranslationProviderEditRequest GetSameValueTranslationProviderEditRequest()
        {
            var request = new TranslationProviderEditRequest(CurrentUserId, UidOne, StringOne, StringTwo);

            return request;
        }

        public static ActiveTranslationProviderRequest GetActiveTranslationProvider()
        {
            var request = new ActiveTranslationProviderRequest();
            request.IsActive = BooleanTrue;
            return request;
        }

        public static OrganizationJournalReadListRequest GetOrganizationJournalReadListRequest()
        {
            var request = new OrganizationJournalReadListRequest(CurrentUserId);

            return request;
        }

        public static OrganizationJournalReadListRequest GetOrganizationJournalReadListRequest(long currentUserId)
        {
            var request = new OrganizationJournalReadListRequest(currentUserId);

            return request;
        }

        public static OrganizationJournalReadListRequest GetOrganizationJournalReadListRequestForSelectAfter()
        {
            var request = GetOrganizationJournalReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static OrganizationJournalReadListRequest GetOrganizationJournalReadListRequestForSelectMany()
        {
            var request = GetOrganizationJournalReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static OrganizationSendEmailLogReadListRequest GetOrganizationSendEmailLogReadListRequest(long currentUserId, Guid organizationUid)
        {
            var request = new OrganizationSendEmailLogReadListRequest(currentUserId, organizationUid);

            return request;
        }

        public static UserJournalReadListRequest GetUserJournalReadListRequest()
        {
            var request = new UserJournalReadListRequest(CurrentUserId, OrganizationOneUserOneUid);

            return request;
        }

        public static UserJournalReadListRequest GetUserJournalReadListRequest(long currentUserId, Guid userUid)
        {
            var request = new UserJournalReadListRequest(currentUserId, userUid);

            return request;
        }

        public static UserJournalReadListRequest GetUserJournalReadListRequestForSelectAfter()
        {
            var request = GetUserJournalReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static UserJournalReadListRequest GetUserJournalReadListRequestForSelectMany()
        {
            var request = GetUserJournalReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static AllUserReadListRequest GetAllUserReadListRequest()
        {
            var request = new AllUserReadListRequest(CurrentUserId);

            return request;
        }

        public static AllUserReadListRequest GetAllUserReadListRequestSelectAfter()
        {
            var request = GetAllUserReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static AllUserReadListRequest GetAllUserReadListRequestSelectMany()
        {
            var request = GetAllUserReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static SuperAdminUserReadListRequest GetSuperAdminUserReadListRequest()
        {
            var request = new SuperAdminUserReadListRequest(CurrentUserId);

            return request;
        }

        public static SuperAdminUserReadListRequest GetSuperAdminUserReadListRequestForSelectAfter()
        {
            var request = GetSuperAdminUserReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static SuperAdminUserReadListRequest GetSuperAdminUserReadListRequestForSelectMany()
        {
            var request = GetSuperAdminUserReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static AdminInviteRequest GetAdminInviteRequest()
        {
            var request = new AdminInviteRequest(CurrentUserId, UidOne, EmailOne,
                                                 StringOne, StringOne);

            return request;
        }

        public static AdminInviteRequest GetAdminInviteRequest(long currentUserId, Guid organizationUid, string email,
                                                               string firstName, string lastName)
        {
            var request = new AdminInviteRequest(currentUserId, organizationUid, email,
                firstName, lastName);

            return request;
        }

        public static AdminInviteValidateRequest GetAdminInviteValidateRequest()
        {
            var request = new AdminInviteValidateRequest(UidOne, EmailOne);

            return request;
        }

        public static AdminInviteValidateRequest GetAdminInviteValidateRequest(Guid token, string email)
        {
            var request = new AdminInviteValidateRequest(token, email);

            return request;
        }

        public static AdminAcceptInviteRequest GetAdminAcceptInviteRequest()
        {
            var request = new AdminAcceptInviteRequest(UidOne, EmailOne, StringOne,
                                                       StringOne, PasswordOne);

            return request;
        }

        public static AdminAcceptInviteRequest GetAdminAcceptInviteRequest(Guid token, string email, string firstName,
                                                                           string lastName, string password)
        {
            var request = new AdminAcceptInviteRequest(token, email, firstName,
                lastName, password);

            return request;
        }

        public static AllJournalReadListRequest GetAllJournalReadListRequest()
        {
            var request = new AllJournalReadListRequest(CurrentUserId);

            return request;
        }

        public static AllJournalReadListRequest GetAllJournalReadListRequest(long currentUserId)
        {
            var request = new AllJournalReadListRequest(currentUserId);

            return request;
        }

        public static AllJournalReadListRequest GetAllJournalReadListRequestForSelectAfter()
        {
            var request = GetAllJournalReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static AllJournalReadListRequest GetAllJournalReadListRequestForSelectMany()
        {
            var request = GetAllJournalReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static AllTokenRequestLogReadListRequest GetAllTokenRequestLogReadListRequest()
        {
            var request = new AllTokenRequestLogReadListRequest(CurrentUserId);

            return request;
        }

        public static AllTokenRequestLogReadListRequest GetAllTokenRequestLogReadListRequestForSelectAfter()
        {
            var request = GetAllTokenRequestLogReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static AllTokenRequestLogReadListRequest GetAllTokenRequestLogReadListRequestForSelectMany()
        {
            var request = GetAllTokenRequestLogReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static AllSendEmailLogReadListRequest GetAllSendEmailLogReadListRequest(long currentUserId)
        {
            var request = new AllSendEmailLogReadListRequest(currentUserId);

            return request;
        }

        public static AllSendEmailLogReadListRequest GetAllSendEmailLogReadListRequest()
        {
            var request = new AllSendEmailLogReadListRequest(CurrentUserId);

            return request;
        }

        public static AllSendEmailLogReadListRequest GetAllSendEmailLogReadListRequestForSelectAfter()
        {
            var request = GetAllSendEmailLogReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static AllSendEmailLogReadListRequest GetAllSendEmailLogReadListRequestForSelectMany()
        {
            var request = GetAllSendEmailLogReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static AllLoginLogReadListRequest GetAllLoginLogReadListRequest()
        {
            var request = new AllLoginLogReadListRequest(CurrentUserId);

            return request;
        }

        public static AllLoginLogReadListRequest GetAllLoginLogReadListRequestForSelectAfter()
        {
            var request = GetAllLoginLogReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static AllLoginLogReadListRequest GetAllLoginLogReadListRequestForSelectMany()
        {
            var request = GetAllLoginLogReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static LabelCreateRequest GetLabelCreateRequest()
        {
            var request = new LabelCreateRequest(CurrentUserId, UidOne, UidOne,
                                                 StringOne, StringOne, GuidArrayOne);

            return request;
        }

        public static LabelCreateRequest GetFromOtherProjectLabelCreateRequest()
        {
            var request = new LabelCreateRequest(CurrentUserId, UidOne, UidOne,
                                                 StringOne, StringOne, GuidArrayOne, BooleanTrue);

            return request;
        }

        public static LabelCreateRequest GetLabelCreateRequestLanguagesUidsZero()
        {
            var request = new LabelCreateRequest(CurrentUserId, UidOne, UidOne, 
                                                 StringOne, StringOne, GuidArrayZero);

            return request;
        }

        public static LabelCreateRequest GetLabelCreateRequest(long currentUserId, Guid organizationUid, Guid projectUid,
                                                               string labelKey, string description, Guid[] languageGuids)
        {
            var request = new LabelCreateRequest(currentUserId, organizationUid, projectUid, 
                                                 labelKey, description, languageGuids);

            return request;
        }

        public static LabelCreateRequest GetLabelCreateRequest(Label label, Project project)
        {
            var request = new LabelCreateRequest(CurrentUserId, label.OrganizationUid, project.Uid,
                                                 label.LabelKey, label.Description, GuidArrayOne);

            return request;
        }

        public static LabelCreateWithTokenRequest GetLabelCreateWithTokenRequestLanguagesIsoCode2Zero()
        {
            var request = new LabelCreateWithTokenRequest(UidOne, UidOne, StringOne, IsoCode2ArrayZero);

            return request;
        }

        public static LabelCreateWithTokenRequest GetLabelCreateWithTokenRequest()
        {
            var request = new LabelCreateWithTokenRequest(UidOne, UidOne, StringOne, IsoCode2ArrayTwo);

            return request;
        }

        public static LabelCreateWithTokenRequest GetLabelCreateWithTokenRequest(Guid token, Guid projectUid, string labelKey, string[] languageNames)
        {
            var request = new LabelCreateWithTokenRequest(token, projectUid, labelKey, languageNames);

            return request;
        }

        public static LabelGetTranslatedTextRequest GetLabelGetTranslatedTextRequest(string textToTranslate, string targetLanguageIsoCode2, string sourceLanguageIsoCode2)
        {
            var request = new LabelGetTranslatedTextRequest(CurrentUserId, textToTranslate, targetLanguageIsoCode2,
                                                            sourceLanguageIsoCode2);

            return request;
        }

        public static LabelCreateListRequest GetLabelCreateListRequestUpdateTrue()
        {
            var request = new LabelCreateListRequest(CurrentUserId, UidOne, UidOne,
                                                     BooleanTrue, new List<LabelListInfo>() { GetLabelListInfo() });

            return request;
        }

        public static LabelCreateListRequest GetLabelCreateListRequestUpdateFalse()
        {
            var request = new LabelCreateListRequest(CurrentUserId, UidOne, UidOne,
                                                     BooleanFalse, new List<LabelListInfo>() { GetLabelListInfo() });

            return request;
        }

        public static LabelCreateListRequest GetLabelCreateListRequest(long currentUserId, Guid organizationUid, Guid projectUid,
                                                                       bool updateExistedTranslations, List<LabelListInfo> labels)
        {
            var request = new LabelCreateListRequest(currentUserId, organizationUid, projectUid, updateExistedTranslations, labels);

            return request;
        }

        public static LabelReadRequest GetLabelReadRequest()
        {
            var request = new LabelReadRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LabelReadRequest GetLabelReadRequest(long currentUserId, Guid labelUid)
        {
            var request = new LabelReadRequest(currentUserId, labelUid);

            return request;
        }

        public static LabelReadByKeyRequest GetLabelReadByKeyRequest()
        {
            var request = new LabelReadByKeyRequest(CurrentUserId, StringOne, StringOne);

            return request;
        }

        public static LabelReadByKeyRequest GetLabelReadByKeyRequest(long currentUserId, string labelKey, string projectName)
        {
            var request = new LabelReadByKeyRequest(currentUserId, labelKey, projectName);

            return request;
        }

        public static LabelReadListRequest GetLabelReadListRequest()
        {
            var request = new LabelReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LabelReadListRequest GetLabelReadListRequest(long currentUserId, Guid projectUid)
        {
            var request = new LabelReadListRequest(currentUserId, projectUid);

            return request;
        }

        public static LabelReadListRequest GetLabelReadListRequestForSelectAfter()
        {
            var request = GetLabelReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static LabelReadListRequest GetLabelReadListRequestForSelectMany()
        {
            var request = GetLabelReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static LabelSearchListRequest GetLabelSearchListRequest()
        {
            var request = new LabelSearchListRequest(CurrentUserId, StringOne);

            return request;
        }

        public static LabelSearchListRequest GetLabelSearchListRequest(long currentUserId, string searchTerm)
        {
            var request = new LabelSearchListRequest(currentUserId, searchTerm);

            return request;
        }

        public static LabelSearchListRequest GetLabelSearchListRequestForSelectMany()
        {
            var request = new LabelSearchListRequest(CurrentUserId, StringOne);
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static LabelRevisionReadListRequest GetLabelRevisionReadListRequest()
        {
            var request = new LabelRevisionReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LabelRevisionReadListRequest GetLabelRevisionReadListRequest(long currentUserId, Guid labelUid)
        {
            var request = new LabelRevisionReadListRequest(currentUserId, labelUid);

            return request;
        }

        public static AllLabelReadListRequest GetAllLabelReadListRequest_IsDefaultProjectTrue()
        {
            var request = new AllLabelReadListRequest(CurrentUserId, UidOne);
            request.IsDefaultProject = BooleanTrue;

            return request;
        }

        public static AllLabelReadListRequest GetAllLabelReadListRequest_IsDefaultProjectFalse()
        {
            var request = new AllLabelReadListRequest(CurrentUserId, UidOne);
            request.IsDefaultProject = BooleanFalse;

            return request;
        }

        public static AllLabelReadListRequest GetAllLabelReadListRequest_IsDefaultProjectTrueAndCurrentUserIdZero()
        {
            var request = GetAllLabelReadListRequest_IsDefaultProjectTrue();
            request.CurrentUserId = Zero;

            return request;
        }

        public static AllLabelReadListRequest GetAllLabelReadListRequest_IsDefaultProjectFalseAndCurrentUserIdZero()
        {
            var request = GetAllLabelReadListRequest_IsDefaultProjectFalse();
            request.CurrentUserId = Zero;

            return request;
        }

        public static AllLabelReadListRequest GetAllLabelReadListRequest(Guid token, Guid projectUid)
        {
            var request = new AllLabelReadListRequest(token, projectUid);

            return request;
        }

        public static AllLabelReadListRequest GetAllLabelReadListRequest(long currentUserId, Guid projectUid)
        {
            var request = new AllLabelReadListRequest(currentUserId, projectUid);

            return request;
        }

        public static LabelEditRequest GetLabelEditRequest()
        {
            var request = new LabelEditRequest(CurrentUserId, UidOne, UidTwo,
                                              UidThree, StringOne, StringTwo);
            return request;
        }

        public static LabelEditRequest GetSameLabelKeyLabelEditRequest()
        {
            var request = new LabelEditRequest(CurrentUserId, OrganizationOneUid, OrganizationOneProjectOneUid,
                                              OrganizationOneProjectOneLabelOneUid, OrganizationOneProjectOneLabelOneKey, StringOne);
            return request;
        }

        public static LabelEditRequest GetLabelEditRequest(long currentUserId, Guid organizationUid, Guid projectUid, Guid labelUid,
                                                           string labelKey, string description)
        {
            var request = new LabelEditRequest(currentUserId, organizationUid, projectUid, labelUid, labelKey,
                description);

            return request;
        }

        public static LabelEditRequest GetLabelEditRequest(Label label)
        {
            var request = new LabelEditRequest(CurrentUserId, label.OrganizationUid, label.ProjectUid, label.Uid,
                label.LabelKey, label.Description);

            return request;
        }

        public static LabelChangeActivationRequest GetLabelChangeActivationRequest()
        {
            var request = new LabelChangeActivationRequest(CurrentUserId, UidOne, UidOne);

            return request;
        }

        public static LabelChangeActivationRequest GetLabelChangeActivationRequest(long currentUserId, Guid organizationUid, Guid labelUid)
        {
            var request = new LabelChangeActivationRequest(currentUserId, organizationUid, labelUid);

            return request;
        }

        public static LabelDeleteRequest GetLabelDeleteRequest()
        {
            var request = new LabelDeleteRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LabelDeleteRequest GetLabelDeleteRequest(long currentUserId, Guid labelUid)
        {
            var request = new LabelDeleteRequest(currentUserId, labelUid);

            return request;
        }

        public static LabelCloneRequest GetLabelCloneRequest()
        {
            var request = new LabelCloneRequest(CurrentUserId, UidOne, UidOne,
                UidOne, StringOne, StringOne);

            return request;
        }

        public static LabelCloneRequest GetLabelCloneRequest(long currentUserId, Guid organizationUid, Guid cloningLabelUid, Guid projectUid,
                                                             string labelKey, string description)
        {
            var request = new LabelCloneRequest(currentUserId, organizationUid, cloningLabelUid,
                projectUid, labelKey, description);

            return request;
        }

        public static LabelCloneRequest GetLabelCloneRequest(Label label)
        {
            var request = new LabelCloneRequest(CurrentUserId, label.OrganizationUid, label.OrganizationUid,
                label.ProjectUid, label.LabelKey, label.Description);

            return request;
        }

        public static LabelRestoreRequest GetLabelRestoreRequest()
        {
            var request = new LabelRestoreRequest(CurrentUserId, UidOne, One);

            return request;
        }

        public static LabelRestoreRequest GetLabelRestoreRequest(long currentUserId, Guid labelUid, int revision)
        {
            var request = new LabelRestoreRequest(currentUserId, labelUid, revision);

            return request;
        }

        public static LabelRestoreRequest GetLabelRestoreRequestRevisionOneInIt()
        {
            var request = new LabelRestoreRequest(CurrentUserId, UidOne, One);

            return request;
        }

        public static LabelRestoreRequest GetLabelRestoreRequestRevisionTwoInIt()
        {
            var request = new LabelRestoreRequest(CurrentUserId, UidOne, Two);

            return request;
        }

        public static LabelTranslationCreateRequest GetLabelTranslationCreateRequest()
        {
            var request = new LabelTranslationCreateRequest(CurrentUserId, UidOne, UidOne,
                                                            UidOne, StringOne);

            return request;
        }

        public static LabelTranslationCreateRequest GetLabelTranslationCreateRequest(long currentUserId, Guid organizationUid,
                                                                                     Guid labelUid, Guid languageUid, string labelTranslation)
        {
            var request = new LabelTranslationCreateRequest(currentUserId, organizationUid, labelUid,
                languageUid, labelTranslation);

            return request;
        }

        public static LabelTranslationCreateListRequest GetLabelTranslationCreateListRequest()
        {
            var request = new LabelTranslationCreateListRequest(CurrentUserId, UidOne, UidOne,
                                                                BooleanTrue, new List<TranslationListInfo>() { GetTranslationListInfo() });

            return request;
        }

        public static LabelTranslationCreateListRequest GetLabelTranslationCreateListRequestUpdateExistedTranslationsFalse()
        {
            var request = new LabelTranslationCreateListRequest(CurrentUserId, UidOne, UidOne,
                BooleanFalse, new List<TranslationListInfo>() { GetTranslationListInfo() });

            return request;
        }


        public static LabelTranslationCreateListRequest GetLabelTranslationCreateListRequest(long currentUserId, Guid organizationUid, Guid labelUid,
                                                                                             bool updateExistedTranslations, List<TranslationListInfo> labelTranslations)
        {
            var request = new LabelTranslationCreateListRequest(currentUserId, organizationUid, labelUid,
                                                                updateExistedTranslations, labelTranslations);

            return request;
        }

        public static LabelTranslationReadRequest GetLabelTranslationReadRequest()
        {
            var request = new LabelTranslationReadRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LabelTranslationReadRequest GetLabelTranslationReadRequest(long currentUserId, Guid labelTranslationUid)
        {
            var request = new LabelTranslationReadRequest(currentUserId, labelTranslationUid);

            return request;
        }

        public static LabelTranslationReadListRequest GetLabelTranslationReadListRequest()
        {
            var request = new LabelTranslationReadListRequest(CurrentUserId, UidOne);
            return request;
        }

        public static LabelTranslationReadListRequest GetLabelTranslationReadListRequest(long currentUserId, Guid labelUid)
        {
            var request = new LabelTranslationReadListRequest(currentUserId, labelUid);

            return request;
        }

        public static LabelTranslationReadListRequest GetLabelTranslationReadListRequestForSelectAfter()
        {
            var request = GetLabelTranslationReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static LabelTranslationReadListRequest GetLabelTranslationReadListRequestForSelectMany()
        {
            var request = GetLabelTranslationReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static LabelTranslationRevisionReadListRequest GetLabelTranslationRevisionReadListRequest()
        {
            var request = new LabelTranslationRevisionReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LabelTranslationRevisionReadListRequest GetLabelTranslationRevisionReadListRequest(long currentUserId, Guid labelTranslationUid)
        {
            var request = new LabelTranslationRevisionReadListRequest(currentUserId, UidOne);

            return request;
        }

        public static LabelTranslationEditRequest GetLabelTranslationEditRequest()
        {
            var request = new LabelTranslationEditRequest(CurrentUserId, UidOne, UidOne,
                                                          StringOne);
            return request;
        }

        public static LabelTranslationEditRequest GetSameTranslationLabelTranslationEditRequest()
        {
            var request = new LabelTranslationEditRequest(CurrentUserId, OrganizationOneUid, UidTwo,
                                                          OrganizationOneProjectOneLabelOneLabelTranslationOneName);
            return request;
        }

        public static LabelTranslationEditRequest GetLabelTranslationEditRequest(long currentUserId, Guid organizationUid, Guid labelTranslationUid,
                                                                                 string newTranslation)
        {
            var request = new LabelTranslationEditRequest(currentUserId, organizationUid, labelTranslationUid,
                newTranslation);

            return request;
        }

        public static LabelTranslationEditRequest GetLabelTranslationEditRequest(LabelTranslation labelTranslation)
        {
            var request = new LabelTranslationEditRequest(CurrentUserId, labelTranslation.OrganizationUid, labelTranslation.LabelUid,
                labelTranslation.TranslationText);

            return request;
        }

        public static LabelTranslationDeleteRequest GetLabelTranslationDeleteRequest()
        {
            var request = new LabelTranslationDeleteRequest(CurrentUserId, UidOne, UidOne);

            return request;
        }

        public static LabelTranslationDeleteRequest GetLabelTranslationDeleteRequest(long currentUserId, Guid organizationUid, Guid labelTranslationUid)
        {
            var request = new LabelTranslationDeleteRequest(currentUserId, organizationUid, labelTranslationUid);

            return request;
        }

        public static LabelTranslationRestoreRequest GetLabelTranslationRestoreRequest()
        {
            var request = new LabelTranslationRestoreRequest(CurrentUserId, UidOne, One);

            return request;
        }

        public static LabelTranslationRestoreRequest GetLabelTranslationRestoreRequest(long currentUserId, Guid labelTranslationUid, int revision)
        {
            var request = new LabelTranslationRestoreRequest(currentUserId, labelTranslationUid, revision);

            return request;
        }

        public static LanguageReadRequest GetLanguageReadRequest()
        {
            var request = new LanguageReadRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LanguageReadRequest GetLanguageReadRequest(long currentUserId, Guid languageUid)
        {
            var request = new LanguageReadRequest(currentUserId, languageUid);

            return request;
        }

        public static LanguageReadListRequest GetLanguageReadListRequest()
        {
            var request = new LanguageReadListRequest();

            return request;
        }

        public static LanguageReadListRequest GetLanguageReadListRequestForSelectAfter()
        {
            var request = GetLanguageReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static LanguageReadListRequest GetLanguageReadListRequestForSelectMany()
        {
            var request = GetLanguageReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static LanguageRevisionReadListRequest GetLanguageRevisionReadListRequest()
        {
            var request = new LanguageRevisionReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LanguageRevisionReadListRequest GetLanguageRevisionReadListRequest(long currentUserId, Guid languageUid)
        {
            var request = new LanguageRevisionReadListRequest(currentUserId, languageUid);

            return request;
        }

        public static LanguageCreateRequest GetLanguageCreateRequest()
        {
            var request = new LanguageCreateRequest(CurrentUserId, StringOne, StringOne,
                                                    IsoCode2One, IsoCode3One, StringOne,
                                                    StringOne);

            return request;
        }

        public static LanguageCreateRequest GetLanguageCreateRequest(long currentUserId, string name, string originalName, string isoCode2,
                                                                     string isoCode3, string icon, string description)
        {
            var request = new LanguageCreateRequest(currentUserId, name, originalName,
                                                    isoCode2, isoCode3, icon,
                                                    description);

            return request;
        }

        public static LanguageEditRequest GetLanguageEditRequest()
        {
            var request = new LanguageEditRequest(CurrentUserId, UidOne, StringOne,
                                                  StringOne, IsoCode2One, IsoCode3One,
                                                  StringOne, StringOne);

            return request;
        }

        public static LanguageEditRequest GetNotDifferentLanguageEditRequest()
        {
            var request = new LanguageEditRequest(CurrentUserId, UidTwo, LanguageTwo,
                                                  LanguageTwoOriginalName, IsoCode2Two, IsoCode3Two,
                                                  StringTwo, StringTwo);

            return request;
        }

        public static LanguageEditRequest GetLanguageEditRequest(long currentUserId, Guid languageUid, string name,
                                                                 string originalName, string isoCode2, string isoCode3,
                                                                 string icon, string description)
        {
            var request = new LanguageEditRequest(currentUserId, languageUid, name,
                                                  originalName, isoCode2, isoCode3,
                                                  icon, description);

            return request;
        }

        public static LanguageEditRequest GetLanguageEditRequest(Language language)
        {
            var request = new LanguageEditRequest(CurrentUserId, language.Uid, language.Name,
                language.OriginalName, language.IsoCode2Char, language.IsoCode3Char,
                language.IconUrl, language.Description);

            return request;
        }

        public static LanguageDeleteRequest GetLanguageDeleteRequest()
        {
            var request = new LanguageDeleteRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LanguageDeleteRequest GetLanguageDeleteRequest(long currentUserId, Guid languageUid)
        {
            var request = new LanguageDeleteRequest(currentUserId, languageUid);

            return request;
        }

        public static LanguageRestoreRequest GetLanguageRestoreRequest()
        {
            var request = new LanguageRestoreRequest(CurrentUserId, UidOne, One);

            return request;
        }

        public static LanguageRestoreRequest GetLanguageRestoreRequest(long currentUserId, Guid languageUid, int revision)
        {
            var request = new LanguageRestoreRequest(currentUserId, languageUid, revision);

            return request;
        }

        public static LanguageRestoreRequest GetLanguageRestoreRequestRevisionOneInIt()
        {
            var request = new LanguageRestoreRequest(CurrentUserId, UidOne, One);
            request.Revision = One;

            return request;
        }

        public static SignUpRequest GetSignUpRequest()
        {
            var request = new SignUpRequest(StringOne, StringOne, StringOne,
                                            EmailOne, PasswordOne, GetClientLogInfo());

            return request;
        }

        public static SignUpRequest GetSignUpRequest(Organization organization)
        {
            var request = new SignUpRequest(organization.Name, OrganizationOneUserOneName,
                                            OrganizationOneName, OrganizationOneUserOneEmail,
                                            PasswordOne, GetClientLogInfo());

            return request;
        }

        public static OrganizationReadRequest GetOrganizationReadRequest()
        {
            var request = new OrganizationReadRequest(CurrentUserId, UidOne);

            return request;
        }

        public static OrganizationReadListRequest GetOrganizationReadListRequest()
        {
            var request = new OrganizationReadListRequest(CurrentUserId);

            return request;
        }

        public static OrganizationReadListRequest GetOrganizationReadListRequestForSelectAfter()
        {
            var request = GetOrganizationReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static OrganizationReadListRequest GetOrganizationReadListRequestForSelectMany()
        {
            var request = GetOrganizationReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static OrganizationRevisionReadListRequest GetOrganizationRevisionReadListRequest()
        {
            var request = new OrganizationRevisionReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static OrganizationEditRequest GetOrganizationEditRequest()
        {
            var request = new OrganizationEditRequest(CurrentUserId, UidOne, StringOne,
                                                      StringOne);

            return request;
        }

        public static OrganizationEditRequest GetNotDifferentOrganizationEditRequest()
        {
            var request = new OrganizationEditRequest(CurrentUserId, OrganizationOneUid, OrganizationOneName,
                                                      StringOne);

            return request;
        }

        public static OrganizationBaseRequest GetOrganizationBaseRequest(long currentUserId, Guid organizationUid)
        {
            var request = new OrganizationBaseRequest(currentUserId, organizationUid);

            return request;
        }

        public static OrganizationPendingTranslationReadListRequest GetOrganizationPendingTranslationReadListRequest(long currentUserId)
        {
            var request = new OrganizationPendingTranslationReadListRequest(currentUserId);

            return request;
        }

        public static SignUpRequest GetSignUpRequest(string organizationName, string firstName, string lastName,
                                                     string email, string password, ClientLogInfo clientLogInfo,
                                                     Guid languageUid = default)
        {
            var request = new SignUpRequest(organizationName, firstName, lastName, email, password, clientLogInfo, languageUid);

            return request;
        }

        public static OrganizationEditRequest GetOrganizationEditRequest(long currentUserId, Guid organizationUid, string name,
                                                                         string description = StringOne)
        {
            var request = new OrganizationEditRequest(currentUserId, organizationUid, name,
                                                      description);

            return request;
        }

        public static OrganizationEditRequest GetOrganizationEditRequest(Organization organization)
        {
            var request = new OrganizationEditRequest(CurrentUserId, organization.Uid, organization.Name, organization.Description);

            return request;
        }

        public static OrganizationRestoreRequest GetOrganizationRestoreRequest(long currentUserId, Guid organizationUid, int revision)
        {
            var request = new OrganizationRestoreRequest(currentUserId, organizationUid, revision);

            return request;
        }

        public static OrganizationRevisionReadListRequest GetOrganizationRevisionReadListRequest(long currentUserId, Guid organizationUid)
        {
            var request = new OrganizationRevisionReadListRequest(currentUserId, organizationUid);

            return request;
        }

        public static OrganizationRestoreRequest GetOrganizationRestoreRequest()
        {
            var request = new OrganizationRestoreRequest(CurrentUserId, UidOne, One);

            return request;
        }

        public static OrganizationPendingTranslationReadListRequest GetOrganizationPendingTranslationReadListRequest()
        {
            var request = new OrganizationPendingTranslationReadListRequest(CurrentUserId);

            return request;
        }

        public static OrganizationPendingTranslationReadListRequest GetOrganizationPendingTranslationReadListRequestForSelectAfter()
        {
            var request = new OrganizationPendingTranslationReadListRequest(CurrentUserId);
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static OrganizationPendingTranslationReadListRequest GetOrganizationPendingTranslationReadListRequestForSelectMany()
        {
            var request = new OrganizationPendingTranslationReadListRequest(CurrentUserId);
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static OrganizationPendingTranslationReadListRequest GetOrganizationPendingTranslationReadListRequestForSelectAfter(Guid organizationUid)
        {
            var request = new OrganizationPendingTranslationReadListRequest(CurrentUserId);
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static OrganizationPendingTranslationReadListRequest GetOrganizationPendingTranslationReadListRequestForSelectMany(Guid organizationUid)
        {
            var request = new OrganizationPendingTranslationReadListRequest(CurrentUserId);
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static ValidateEmailRequest GetValidateEmailRequest()
        {
            var request = new ValidateEmailRequest(UidOne, EmailOne);

            return request;
        }

        public static ValidateEmailRequest GetValidateEmailRequest(Guid token, string email)
        {
            var request = new ValidateEmailRequest(token, email);

            return request;
        }

        public static LogOnRequest GetLogOnRequest()
        {
            var request = new LogOnRequest(EmailOne, PasswordOne, GetClientLogInfo());

            return request;
        }

        public static LogOnRequest GetLogOnRequest(string email, string password, ClientLogInfo clientLogInfo)
        {
            var request = new LogOnRequest(email, password, clientLogInfo);

            return request;
        }

        public static DemandPasswordResetRequest GetDemandPasswordResetRequest()
        {
            var request = new DemandPasswordResetRequest(EmailOne);

            return request;
        }

        public static DemandPasswordResetRequest GetDemandPasswordResetRequest(string email)
        {
            var request = new DemandPasswordResetRequest(email);

            return request;
        }

        public static PasswordResetValidateRequest GetPasswordResetValidateRequest()
        {
            var request = new PasswordResetValidateRequest(UidOne, EmailOne);

            return request;
        }

        public static PasswordResetValidateRequest GetPasswordResetValidateRequest(Guid token, string email)
        {
            var request = new PasswordResetValidateRequest(token, email);

            return request;
        }

        public static PasswordResetRequest GetPasswordResetRequest()
        {
            var request = new PasswordResetRequest(UidOne, EmailOne, PasswordOne);

            return request;
        }

        public static PasswordResetRequest GetPasswordResetRequest(Guid token, string email, string password)
        {
            var request = new PasswordResetRequest(token, email, password);

            return request;
        }

        public static PasswordChangeRequest GetPasswordChangeRequest()
        {
            var request = new PasswordChangeRequest(CurrentUserId, PasswordOne, PasswordTwo);

            return request;
        }

        public static PasswordChangeRequest GetPasswordChangeRequest(long currentUserId, string oldPassword, string newPassword)
        {
            var request = new PasswordChangeRequest(currentUserId, oldPassword, newPassword);

            return request;
        }

        public static PasswordChangeRequest GetPasswordChangeRequest(string oldPassword, string newPassword)
        {
            var request = new PasswordChangeRequest(CurrentUserId, oldPassword, newPassword);

            return request;
        }

        public static OrganizationLoginLogReadListRequest GetOrganizationLoginLogReadListRequest()
        {
            var request = new OrganizationLoginLogReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static OrganizationLoginLogReadListRequest GetOrganizationLoginLogReadListRequestForSelectAfter()
        {
            var request = GetOrganizationLoginLogReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static OrganizationLoginLogReadListRequest GetOrganizationLoginLogReadListRequestForSelectMany()
        {
            var request = GetOrganizationLoginLogReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static OrganizationLoginLogReadListRequest GetOrganizationLoginLogReadListRequest(long currentUserId, Guid organizationUid)
        {
            var request = new OrganizationLoginLogReadListRequest(currentUserId, organizationUid);

            return request;
        }

        public static CurrentUserRequest GetCurrentUserRequest()
        {
            var request = new CurrentUserRequest(EmailOne);

            return request;
        }

        public static CurrentUserRequest GetCurrentUserRequest(string email)
        {
            var request = new CurrentUserRequest(email);

            return request;
        }

        public static UserRestoreRequest GetUserRestoreRequest()
        {
            var request = new UserRestoreRequest(CurrentUserId, UidOne, One);

            return request;
        }

        public static UserRestoreRequest GetUserRestoreRequest(long currentUserId, Guid userUid, int revision)
        {
            var request = new UserRestoreRequest(currentUserId, userUid, revision);

            return request;
        }

        public static UserRestoreRequest GetUserRestoreRequest(Guid userUid, int revision)
        {
            var request = new UserRestoreRequest(CurrentUserId, userUid, revision);

            return request;
        }

        public static UserEditRequest GetUserEditRequest()
        {
            var request = new UserEditRequest(CurrentUserId, UidOne, StringOne, StringOne, UidOne);

            return request;
        }

        public static UserEditRequest GetNotDifferentUserEditRequest()
        {
            var request = new UserEditRequest(CurrentUserId, OrganizationOneUserOneUid, OrganizationOneUserOneName,
                                              OrganizationOneUserOneName, UidOne);

            return request;
        }

        public static UserEditRequest GetUserEditRequest(long currentUserId, Guid userUid, string firsName,
                                                        string lastName, Guid languageUid)
        {
            var request = new UserEditRequest(currentUserId, userUid, firsName, lastName, languageUid);

            return request;
        }

        public static UserBaseRequest GetUserBaseRequest(long currentUserId, Guid userUid)
        {
            var request = new UserBaseRequest(currentUserId, userUid);

            return request;
        }

        public static UserDeleteRequest GetUserDeleteRequest()
        {
            var request = new UserDeleteRequest(CurrentUserId, UidOne);

            return request;
        }

        public static UserInviteRequest GetUserInviteRequest()
        {
            var request = new UserInviteRequest(CurrentUserId, UidOne, EmailOne, StringOne, StringOne);

            return request;
        }

        public static UserInviteRequest GetUserInviteRequest(long currentUserId, Guid organizationUid, string email,
                                                             string firstName, string lastName)
        {
            var request = new UserInviteRequest(currentUserId, organizationUid, email, firstName, lastName);

            return request;
        }

        public static UserInviteValidateRequest GetUserInviteValidateRequest()
        {
            var request = new UserInviteValidateRequest(UidOne, EmailOne);

            return request;
        }

        public static UserInviteValidateRequest GetUserInviteValidateRequest(Guid token, string email)
        {
            var request = new UserInviteValidateRequest(token, email);

            return request;
        }

        public static UserAcceptInviteRequest GetUserAcceptInviteRequest()
        {
            var request = new UserAcceptInviteRequest(UidOne, EmailOne, StringOne, StringOne, PasswordOne, StringOne, UidOne);

            return request;
        }

        public static UserAcceptInviteRequest GetUserAcceptInviteRequest(Guid token, string email, string firstName,
                                                                         string lastName, string password, string languageName,
                                                                         Guid languageUid)
        {
            var request = new UserAcceptInviteRequest(token, email, firstName, lastName, password, languageName, languageUid);

            return request;
        }

        public static UserReadRequest GetUserReadRequest()
        {
            var request = new UserReadRequest(CurrentUserId, UidOne);

            return request;
        }

        public static UserReadRequest GetUserReadRequest(long currentUserId, Guid userUid)
        {
            var request = new UserReadRequest(currentUserId, userUid);

            return request;
        }

        public static UserReadListRequest GetUserReadListRequest()
        {
            var request = new UserReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static UserReadListRequest GetUserReadListRequestForSelectAfter()
        {
            var request = GetUserReadListRequest();
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static UserReadListRequest GetUserReadListRequestForSelectMany()
        {
            var request = GetUserReadListRequest();
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static UserReadListRequest GetUserReadListRequest(long currentUserId, Guid organizationUid)
        {
            var request = new UserReadListRequest(currentUserId, organizationUid);

            return request;
        }

        public static UserRevisionReadListRequest GetUserRevisionReadListRequest()
        {
            var request = new UserRevisionReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static UserRevisionReadListRequest GetUserRevisionReadListRequest(long currentUserId, Guid userUid)
        {
            var request = new UserRevisionReadListRequest(currentUserId, userUid);

            return request;
        }

        public static UserLoginLogReadListRequest GetUserLoginLogReadListRequest()
        {
            var request = new UserLoginLogReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static UserLoginLogReadListRequest GetUserLoginLogReadListRequest(long currentUserId, Guid userUid)
        {
            var request = new UserLoginLogReadListRequest(currentUserId, userUid);

            return request;
        }

        public static UserLoginLogReadListRequest GetUserLoginLogReadListRequestForSelectAfter()
        {
            var request = new UserLoginLogReadListRequest(CurrentUserId, UidOne);
            SetPagingInfoForSelectAfter(request.PagingInfo);

            return request;
        }

        public static UserLoginLogReadListRequest GetUserLoginLogReadListRequestForSelectMany()
        {
            var request = new UserLoginLogReadListRequest(CurrentUserId, UidOne);
            SetPagingInfoForSelectMany(request.PagingInfo);

            return request;
        }

        public static void SetPagingInfoForSelectAfter(PagingInfo pagingInfo)
        {
            pagingInfo.Skip = Zero;
            pagingInfo.Take = OneHundred;
            pagingInfo.IsAscending = BooleanTrue;
            pagingInfo.LastUid = UidOne;
            pagingInfo.TotalItemCount = Ten;
            pagingInfo.SearchTerm = StringOne;

        }

        public static void SetPagingInfoForSelectMany(PagingInfo pagingInfo)
        {
            pagingInfo.Skip = One;
            pagingInfo.Take = OneHundred;
            pagingInfo.IsAscending = BooleanTrue;
            pagingInfo.TotalItemCount = Ten;
            pagingInfo.LastUid = UidOne;
            pagingInfo.TotalItemCount = Ten;
            pagingInfo.SearchTerm = StringOne;
        }
    }
}