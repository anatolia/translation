using Translation.Common.Models.Requests.Integration;
using Translation.Common.Models.Requests.Integration.IntegrationClient;
using Translation.Common.Models.Requests.Project;
using Translation.Common.Models.Responses.Integration;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Tests.TestHelpers
{
    public class FakeRequestTestHelper
    {
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
            var request = new IntegrationClientRefreshRequest(CurrentUserId,OrganizationOneIntegrationOneIntegrationClientOneUid);

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
            var request = new IntegrationReadListRequest(CurrentUserId, OrganizationOneUid);
            request.PagingInfo.Skip = 0;

            return request;
        }

        public static IntegrationReadListRequest GetIntegrationReadListRequestForSelectMany()
        {

            var request = new IntegrationReadListRequest(CurrentUserId, OrganizationOneUid);
            request.PagingInfo.Skip = 1;

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
            var request = new ProjectReadRequest(CurrentUserId,OrganizationOneProjectOneUid);

            return request;
        }

        public static ProjectDeleteRequest GetProjectDeleteRequest()
        {
            var request = new ProjectDeleteRequest(CurrentUserId,OrganizationOneProjectOneUid);

            return request;
        }

        public static ProjectChangeActivationRequest GetProjectChangeActivationRequest()
        {
            var request = new ProjectChangeActivationRequest(CurrentUserId, OrganizationOneUid, OrganizationOneProjectOneUid);

            return request;
        }

        public static ProjectRestoreRequest GetProjectRestoreRequest()
        {
            var request = new ProjectRestoreRequest(CurrentUserId,OrganizationOneUid, One);

            return request;
        }

        public static ProjectRevisionReadListRequest GetProjectRevisionReadListRequest()
        {
            var request = new ProjectRevisionReadListRequest(CurrentUserId,OrganizationOneProjectOneUid);

            return request;
        }

        public static ProjectPendingTranslationReadListRequest GetProjectPendingTranslationReadListRequest()
        {
            var request = new ProjectPendingTranslationReadListRequest(CurrentUserId,OrganizationOneProjectOneUid);

            return request;
        }

    }
}