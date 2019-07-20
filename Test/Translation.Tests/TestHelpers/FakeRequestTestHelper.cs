using System.Collections.Generic;
using System.Net;
using Translation.Common.Models.Requests.Admin;
using Translation.Common.Models.Requests.Integration;
using Translation.Common.Models.Requests.Journal;
using Translation.Common.Models.Requests.Integration.IntegrationClient;
using Translation.Common.Models.Requests.Integration.Token;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Requests.Label.LabelTranslation;
using Translation.Common.Models.Requests.Language;
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Requests.Project;
using Translation.Common.Models.Requests.SendEmailLog;
using Translation.Common.Models.Requests.User;
using Translation.Common.Models.Requests.User.LoginLog;
using Translation.Common.Models.Responses.Admin;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Tests.TestHelpers
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

        public static OrganizationActiveTokenReadListRequest GetOrganizationActiveTokenReadListRequest()
        {
            var request = new OrganizationActiveTokenReadListRequest(CurrentUserId, UidOne);

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

        public static TokenCreateRequest GetTokenCreateRequest()
        {
            var request = new TokenCreateRequest(UidOne, UidOne, IPAddress.Any);

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

        public static TokenValidateRequest GetTokenValidateRequest()
        {
            var request = new TokenValidateRequest(UidOne, UidOne);

            return request;
        }

        public static JournalCreateRequest GetJournalCreateRequest()
        {
            var request = new JournalCreateRequest(CurrentUserId, StringOne);

            return request;
        }

        public static IntegrationClientChangeActivationRequest GetIntegrationClientChangeActivationRequest()
        {
            var request = new IntegrationClientChangeActivationRequest(CurrentUserId, OrganizationOneIntegrationOneIntegrationClientOneUid);

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

        public static IntegrationClientCreateRequest GetIntegrationClientCreateRequest()
        {

            var request = new IntegrationClientCreateRequest(CurrentUserId, UidOne);

            return request;
        }

        public static IntegrationRestoreRequest GetIntegrationRestoreRequest()
        {
            var request = new IntegrationRestoreRequest(CurrentUserId, UidOne, One);

            return request;
        }

        public static IntegrationChangeActivationRequest GetIntegrationChangeActivationRequest()
        {
            var request = new IntegrationChangeActivationRequest(CurrentUserId, UidOne);

            return request;
        }

        public static IntegrationCreateRequest GetIntegrationCreateRequest()
        {
            var request = new IntegrationCreateRequest(CurrentUserId, OrganizationOneUid, StringOne,
                                                      StringOne);

            return request;
        }

        public static IntegrationReadListRequest GetIntegrationReadListRequest()
        {
            var request = new IntegrationReadListRequest(CurrentUserId, OrganizationOneUid);

            return request;
        }

        public static IntegrationClientReadListRequest GetIntegrationClientReadListRequestForSelectAfter()
        {
            var request = GetIntegrationClientReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectAfter();

            return request;
        }

        public static IntegrationClientReadListRequest GetIntegrationClientReadListRequestForSelectMany()
        {
            var request = GetIntegrationClientReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectMany();

            return request;
        }

        public static IntegrationReadListRequest GetIntegrationReadListRequestForSelectAfter()
        {
            var request = GetIntegrationReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectAfter();

            return request;
        }

        public static IntegrationReadListRequest GetIntegrationReadListRequestForSelectMany()
        {
            var request = GetIntegrationReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectMany();

            return request;
        }

        public static IntegrationRevisionReadListRequest GetIntegrationRevisionReadListRequest()
        {
            var request = new IntegrationRevisionReadListRequest(CurrentUserId, OrganizationOneIntegrationOneUid);

            return request;
        }

        public static IntegrationReadRequest GetIntegrationReadRequest()
        {
            var request = new IntegrationReadRequest(CurrentUserId, OrganizationOneIntegrationOneUid);

            return request;
        }

        public static IntegrationEditRequest GetIntegrationEditRequest()
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

        public static ProjectCreateRequest GetProjectCreateRequest()
        {
            var request = new ProjectCreateRequest(CurrentUserId, OrganizationOneUid, StringOne,
                                                   HttpUrl, StringOne, StringOne);

            return request;
        }

        public static ProjectCreateRequest GetProjectCreateRequest(Organization organization, Project project)
        {
            var request = new ProjectCreateRequest(CurrentUserId, organization.Uid, project.Name,
                                                   project.Url, project.Description, project.Slug);

            return request;
        }

        public static ProjectCreateRequest GetProjectCreateRequest(CurrentOrganization organization, Project project)
        {
            var request = new ProjectCreateRequest(CurrentUserId, organization.Uid, project.Name,
                                                   project.Url, project.Description, project.Slug);

            return request;
        }

        public static ProjectEditRequest GetProjectEditRequest()
        {
            var request = new ProjectEditRequest(CurrentUserId, OrganizationOneUid, UidOne,
                                                 StringOne, HttpUrl, StringOne,
                                                 StringOne);

            return request;
        }

        public static ProjectEditRequest GetProjectEditRequest(Project project)
        {
            var request = new ProjectEditRequest(CurrentUserId, project.OrganizationUid, project.Uid,
                                                 project.Name, project.Url, project.Description,
                                                 project.Slug);

            return request;
        }

        public static ProjectCloneRequest GetProjectCloneRequest()
        {
            var request = new ProjectCloneRequest(CurrentUserId, OrganizationOneUid, UidOne,
                                                  StringOne, HttpUrl, StringOne,
                                                  One, Two, BooleanTrue,
                                                  StringOne);

            return request;
        }

        public static ProjectCloneRequest GetProjectCloneRequest(Project project)
        {
            var request = new ProjectCloneRequest(CurrentUserId, project.OrganizationUid, project.Uid,
                                                  project.Name, project.Url, project.Description,
                                                  project.LabelCount, project.LabelTranslationCount, project.IsSuperProject,
                                                  project.Slug);

            return request;
        }

        public static ProjectReadListRequest GetProjectReadListRequest()
        {
            var request = new ProjectReadListRequest(CurrentUserId, OrganizationOneUid);

            return request;
        }

        public static ProjectReadListRequest GetProjectReadListRequestForSelectAfter()
        {
            var request = GetProjectReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectAfter();

            return request;
        }

        public static ProjectReadListRequest GetProjectReadListRequestForSelectMany()
        {
            var request = GetProjectReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectMany();

            return request;
        }

        public static ProjectReadRequest GetProjectReadRequest()
        {
            var request = new ProjectReadRequest(CurrentUserId, OrganizationOneProjectOneUid);

            return request;
        }

        public static ProjectReadBySlugRequest GetProjectReadBySlugRequest()
        {
            var request = new ProjectReadBySlugRequest(CurrentUserId, StringOne);

            return request;
        }

        public static ProjectDeleteRequest GetProjectDeleteRequest()
        {
            var request = new ProjectDeleteRequest(CurrentUserId, OrganizationOneProjectOneUid);

            return request;
        }

        public static ProjectChangeActivationRequest GetProjectChangeActivationRequest()
        {
            var request = new ProjectChangeActivationRequest(CurrentUserId, OrganizationOneUid, OrganizationOneProjectOneUid);

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

        public static ProjectRestoreRequest GetProjectRestoreRequest()
        {
            var request = new ProjectRestoreRequest(CurrentUserId, OrganizationOneUid, One);

            return request;
        }

        public static ProjectRevisionReadListRequest GetProjectRevisionReadListRequest()
        {
            var request = new ProjectRevisionReadListRequest(CurrentUserId, OrganizationOneProjectOneUid);

            return request;
        }

        public static ProjectPendingTranslationReadListRequest GetProjectPendingTranslationReadListRequest()
        {
            var request = new ProjectPendingTranslationReadListRequest(CurrentUserId, OrganizationOneProjectOneUid);

            return request;
        }

        public static ProjectPendingTranslationReadListRequest GetProjectPendingTranslationReadListRequestForSelectAfter()
        {
            var request = GetProjectPendingTranslationReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectAfter();

            return request;
        }

        public static ProjectPendingTranslationReadListRequest GetProjectPendingTranslationReadListRequestForSelectMany()
        {
            var request = GetProjectPendingTranslationReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectMany();

            return request;
        }

        public static OrganizationJournalReadListRequest GetOrganizationJournalReadListRequest()
        {
            var request = new OrganizationJournalReadListRequest(CurrentUserId, OrganizationOneUid);

            return request;
        }

        public static OrganizationJournalReadListRequest GetOrganizationJournalReadListRequestForSelectAfter()
        {
            var request = GetOrganizationJournalReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectAfter();

            return request;
        }

        public static OrganizationJournalReadListRequest GetOrganizationJournalReadListRequestForSelectMany()
        {
            var request = GetOrganizationJournalReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectMany();

            return request;
        }

        public static UserJournalReadListRequest GetUserJournalReadListRequest()
        {
            var request = new UserJournalReadListRequest(CurrentUserId, OrganizationOneUserOneUid);

            return request;
        }

        public static UserJournalReadListRequest GetUserJournalReadListRequestForSelectAfter()
        {
            var request = GetUserJournalReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectAfter();

            return request;
        }

        public static UserJournalReadListRequest GetUserJournalReadListRequestForSelectMany()
        {
            var request = GetUserJournalReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectMany();

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
            request.PagingInfo = GetPagingInfoForSelectAfter();

            return request;
        }

        public static AllUserReadListRequest GetAllUserReadListRequestSelectMany()
        {
            var request = GetAllUserReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectMany();

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
            request.PagingInfo = GetPagingInfoForSelectAfter();

            return request;
        }

        public static SuperAdminUserReadListRequest GetSuperAdminUserReadListRequestForSelectMany()
        {
            var request = GetSuperAdminUserReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectMany();

            return request;
        }

        public static AdminInviteRequest GetAdminInviteRequest()
        {
            var request = new AdminInviteRequest(CurrentUserId, UidOne, EmailOne,
                                                 StringOne, StringOne);

            return request;
        }

        public static AdminInviteValidateRequest GetAdminInviteValidateRequest()
        {
            var request = new AdminInviteValidateRequest(UidOne, EmailOne);

            return request;
        }

        public static AdminAcceptInviteRequest GetAdminAcceptInviteRequest()
        {
            var request = new AdminAcceptInviteRequest(UidOne, EmailOne, StringOne,
                                                       StringOne, PasswordOne);

            return request;
        }

        public static AllJournalReadListRequest GetAllJournalReadListRequest()
        {
            var request = new AllJournalReadListRequest(CurrentUserId);

            return request;
        }

        public static AllJournalReadListRequest GetAllJournalReadListRequestForSelectAfter()
        {
            var request = GetAllJournalReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectAfter();

            return request;
        }

        public static AllJournalReadListRequest GetAllJournalReadListRequestForSelectMany()
        {
            var request = GetAllJournalReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectMany();

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
            request.PagingInfo = GetPagingInfoForSelectAfter();

            return request;
        }

        public static AllTokenRequestLogReadListRequest GetAllTokenRequestLogReadListRequestForSelectMany()
        {
            var request = GetAllTokenRequestLogReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectMany();

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
            request.PagingInfo = GetPagingInfoForSelectAfter();

            return request;
        }

        public static AllSendEmailLogReadListRequest GetAllSendEmailLogReadListRequestForSelectMany()
        {
            var request = GetAllSendEmailLogReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectMany();

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
            request.PagingInfo = GetPagingInfoForSelectAfter();

            return request;
        }

        public static AllLoginLogReadListRequest GetAllLoginLogReadListRequestForSelectMany()
        {
            var request = GetAllLoginLogReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectMany();

            return request;
        }

        public static LabelCreateRequest GetLabelCreateRequest()
        {
            var request = new LabelCreateRequest(CurrentUserId, UidOne, UidOne,
                                                 StringOne, StringOne);

            return request;
        }
        
        public static LabelCreateWithTokenRequest GetLabelCreateWithTokenRequest()
        {
            var request = new LabelCreateWithTokenRequest(UidOne, UidOne, StringOne);

            return request;
        }

        public static LabelCreateListRequest GetLabelCreateListRequest()
        {
            var request = new LabelCreateListRequest(CurrentUserId, UidOne, UidOne,
                                                     new List<LabelListInfo>() { GetLabelListInfo() });

            return request;
        }

        public static LabelReadRequest GetLabelReadRequest()
        {
            var request = new LabelReadRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LabelReadByKeyRequest GetLabelReadByKeyRequest()
        {
            var request = new LabelReadByKeyRequest(CurrentUserId, StringOne, StringOne);

            return request;
        }

        public static LabelReadListRequest GetLabelReadListRequest()
        {
            var request = new LabelReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LabelReadListRequest GetLabelReadListRequestForSelectAfter()
        {
            var request = GetLabelReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectAfter();

            return request;
        }

        public static LabelReadListRequest GetLabelReadListRequestForSelectMany()
        {
            var request = GetLabelReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectMany();

            return request;
        }

        public static LabelSearchListRequest GetLabelSearchListRequest()
        {
            var request = new LabelSearchListRequest(CurrentUserId, StringOne);

            return request;
        }

        public static LabelSearchListRequest GetLabelSearchListRequestForSelectMany()
        {
            var request = new LabelSearchListRequest(CurrentUserId, StringOne);
            request.PagingInfo = GetPagingInfoForSelectMany();

            return request;
        }

        public static LabelRevisionReadListRequest GetLabelRevisionReadListRequest()
        {
            var request = new LabelRevisionReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static AllLabelReadListRequest GetAllLabelReadListRequest()
        {
            var request = new AllLabelReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LabelEditRequest GetLabelEditRequest()
        {
            var request = new LabelEditRequest(CurrentUserId, UidOne, UidOne, StringOne,
                                               StringOne);

            return request;
        }

        public static LabelChangeActivationRequest GetLabelChangeActivationRequest()
        {
            var request = new LabelChangeActivationRequest(CurrentUserId, UidOne, UidOne);

            return request;
        }

        public static LabelDeleteRequest GetLabelDeleteRequest()
        {
            var request = new LabelDeleteRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LabelCloneRequest GetLabelCloneRequest()
        {
            var request = new LabelCloneRequest(CurrentUserId, UidOne, UidOne,
                                                UidOne, StringOne, StringOne);

            return request;
        }

        public static LabelRestoreRequest GetLabelRestoreRequest()
        {
            var request = new LabelRestoreRequest(CurrentUserId, UidOne, One);

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

        public static LabelTranslationCreateListRequest GetLabelTranslationCreateListRequest()
        {
            var request = new LabelTranslationCreateListRequest(CurrentUserId, UidOne, UidOne,
                                                                new List<TranslationListInfo>() { GetTranslationListInfo() });

            return request;
        }

        public static LabelTranslationReadRequest GetLabelTranslationReadRequest()
        {
            var request = new LabelTranslationReadRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LabelTranslationReadListRequest GetLabelTranslationReadListRequest()
        {
            var request = new LabelTranslationReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LabelTranslationReadListRequest GetLabelTranslationReadListRequestForSelectAfter()
        {
            var request = GetLabelTranslationReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectAfter();

            return request;
        }

        public static LabelTranslationReadListRequest GetLabelTranslationReadListRequestForSelectMany()
        {
            var request = GetLabelTranslationReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectMany();

            return request;
        }

        public static LabelTranslationRevisionReadListRequest GetLabelTranslationRevisionReadListRequest()
        {
            var request = new LabelTranslationRevisionReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LabelTranslationEditRequest GetLabelTranslationEditRequest()
        {
            var request = new LabelTranslationEditRequest(CurrentUserId, UidOne, UidOne,
                                                          StringOne);

            return request;
        }

        public static LabelTranslationDeleteRequest GetLabelTranslationDeleteRequest()
        {
            var request = new LabelTranslationDeleteRequest(CurrentUserId, UidOne, UidOne);

            return request;
        }

        public static LabelTranslationRestoreRequest GetLabelTranslationRestoreRequest()
        {
            var request = new LabelTranslationRestoreRequest(CurrentUserId, UidOne, One);

            return request;
        }

        public static LanguageReadRequest GetLanguageReadRequest()
        {
            var request = new LanguageReadRequest(CurrentUserId, UidOne);

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
            request.PagingInfo = GetPagingInfoForSelectAfter();

            return request;
        }

        public static LanguageReadListRequest GetLanguageReadListRequestForSelectMany()
        {
            var request = GetLanguageReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectMany();

            return request;
        }

        public static LanguageRevisionReadListRequest GetLanguageRevisionReadListRequest()
        {
            var request = new LanguageRevisionReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LanguageCreateRequest GetLanguageCreateRequest()
        {
            var request = new LanguageCreateRequest(CurrentUserId, StringOne, StringOne,
                                                    IsoCode2One, IsoCode3One, StringOne,
                                                    StringOne);

            return request;
        }

        public static LanguageEditRequest GetLanguageEditRequest()
        {
            var request = new LanguageEditRequest(CurrentUserId, UidOne, StringOne,
                                                  StringOne, IsoCode2One, IsoCode3One,
                                                  StringOne, StringOne);

            return request;
        }

        public static LanguageDeleteRequest GetLanguageDeleteRequest()
        {
            var request = new LanguageDeleteRequest(CurrentUserId, UidOne);

            return request;
        }

        public static LanguageRestoreRequest GetLanguageRestoreRequest()
        {
            var request = new LanguageRestoreRequest(CurrentUserId, UidOne, One);

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
            request.PagingInfo = GetPagingInfoForSelectAfter();

            return request;
        }

        public static OrganizationReadListRequest GetOrganizationReadListRequestForSelectMany()
        {
            var request = GetOrganizationReadListRequest();
            request.PagingInfo = GetPagingInfoForSelectMany();

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

        public static OrganizationRestoreRequest GetOrganizationRestoreRequest()
        {
            var request = new OrganizationRestoreRequest(CurrentUserId, UidOne, One);

            return request;
        }

        public static OrganizationPendingTranslationReadListRequest GetOrganizationPendingTranslationReadListRequest()
        {
            var request = new OrganizationPendingTranslationReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static ValidateEmailRequest GetValidateEmailRequest()
        {
            var request = new ValidateEmailRequest(UidOne, EmailOne);

            return request;
        }

        public static LogOnRequest GetLogOnRequest()
        {
            var request = new LogOnRequest(EmailOne, PasswordOne, GetClientLogInfo());

            return request;
        }

        public static DemandPasswordResetRequest GetDemandPasswordResetRequest()
        {
            var request = new DemandPasswordResetRequest(EmailOne);

            return request;
        }

        public static UserLoginLogReadListRequest GetUserLoginLogReadListRequest()
        {
            var request = new UserLoginLogReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static OrganizationLoginLogReadListRequest GetOrganizationLoginLogReadListRequest()
        {
            var request = new OrganizationLoginLogReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static CurrentUserRequest GetCurrentUserRequest()
        {
            var request = new CurrentUserRequest(EmailOne);

            return request;
        }

        public static UserRestoreRequest GetUserRestoreRequest()
        {
            var request = new UserRestoreRequest(CurrentUserId, UidOne, One);

            return request;
        }

        public static UserReadListRequest GetUserReadListRequest()
        {
            var request = new UserReadListRequest(CurrentUserId, UidOne);

            return request;
        }

        public static UserRevisionReadListRequest GetUserRevisionReadListRequest()
        {
            var request = new UserRevisionReadListRequest(CurrentUserId, UidOne);

            return request;
        }
    }
}