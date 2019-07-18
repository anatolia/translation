using Translation.Common.Models.Requests.Admin;
using Translation.Common.Models.Requests.Integration;
using Translation.Common.Models.Requests.Journal;
using Translation.Common.Models.Requests.Integration.IntegrationClient;
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Requests.Project;
using Translation.Common.Models.Requests.User;
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
            var request = new ProjectCreateRequest(CurrentUserId, OrganizationOneUid, StringOne, HttpUrl, StringOne);

            return request;
        }

        public static ProjectCreateRequest GetProjectCreateRequest(Organization organization, Project project)
        {
            var request = new ProjectCreateRequest(CurrentUserId, organization.Uid, project.Name,
                project.Url, project.Description);

            return request;
        }

        public static ProjectCreateRequest GetProjectCreateRequest(CurrentOrganization organization, Project project)
        {
            var request = new ProjectCreateRequest(CurrentUserId, organization.Uid, project.Name,
                project.Url, project.Description);

            return request;
        }

        public static ProjectEditRequest GetProjectEditRequest()
        {
            var request = new ProjectEditRequest(CurrentUserId, OrganizationOneUid, UidOne,
                                                 StringOne, HttpUrl, StringOne);

            return request;
        }

        public static ProjectEditRequest GetProjectEditRequest(Project project)
        {
            var request = new ProjectEditRequest(CurrentUserId, project.OrganizationUid, project.Uid,
                project.Name, project.Url, project.Description);

            return request;
        }

        public static ProjectCloneRequest GetProjectCloneRequest()
        {
            var request = new ProjectCloneRequest(CurrentUserId, OrganizationOneUid, UidOne, StringOne,
                HttpUrl, StringOne, One, Two, BooleanTrue);

            return request;
        }

        public static ProjectCloneRequest GetProjectCloneRequest(Project project)
        {
            var request = new ProjectCloneRequest(CurrentUserId, project.OrganizationUid, project.Uid,
                project.Name, project.Url, project.Description,
                project.LabelCount, project.LabelTranslationCount, project.IsSuperProject);

            return request;
        }

        public static ProjectReadListRequest GetProjectReadListRequest()
        {
            var request = new ProjectReadListRequest(CurrentUserId, OrganizationOneUid);

            return request;
        }

        public static ProjectReadRequest GetProjectReadRequest()
        {
            var request = new ProjectReadRequest(CurrentUserId, OrganizationOneProjectOneUid);

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

        public static OrganizationJournalReadListRequest GetOrganizationJournalReadListRequest()
        {
            var organization = GetOrganizationOne();
            var request = new OrganizationJournalReadListRequest(CurrentUserId, organization.Uid);

            return request;
        }

        public static UserJournalReadListRequest GetUserJournalReadListRequest()
        {
            var request = new UserJournalReadListRequest(CurrentUserId, OrganizationOneUserOneUid);

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
            var request = new AdminInviteRequest(CurrentUserId, UidOne, EmailOne, StringOne, StringOne);

            return request;
        }

        public static AdminInviteValidateRequest GetAdminInviteValidateRequest()
        {
            var request = new AdminInviteValidateRequest(UidOne, EmailOne);

            return request;
        }

        public static AdminAcceptInviteRequest GetAdminAcceptInviteRequest()
        {
            var request = new AdminAcceptInviteRequest(UidOne, EmailOne, StringOne, StringOne, PasswordOne);

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
    }
}