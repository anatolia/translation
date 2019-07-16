using Translation.Common.Models.Requests.Integration;
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
        public static IntegrationRestoreRequest GetIntegrationRestoreRequest()
        {
            var integration = GetOrganizationOneIntegrationOne();
            var request = new IntegrationRestoreRequest(CurrentUserId,integration.Uid,One);
            return request;
        }

        public static IntegrationChangeActivationRequest GetIntegrationChangeActivationRequest()
        {
            var integration = GetOrganizationOneIntegrationOne();
            var request = new IntegrationChangeActivationRequest(CurrentUserId,integration.Uid);

            return request;
        }

        public static IntegrationCreateRequest GetIntegrationCreateRequest()
        {
            var integration = GetOrganizationOneIntegrationOne();
            var request = new IntegrationCreateRequest(CurrentUserId, integration.OrganizationUid, integration.Name,
                                                      integration.Description);
            return request;
        }

        public static IntegrationReadListRequest GetIntegrationReadListRequest()
        {
            var organization = GetOrganizationOne();
            var request = new IntegrationReadListRequest(CurrentUserId, organization.Uid);

            return request;
        }

        public static IntegrationReadListRequest GetIntegrationReadListRequestForSelectAfter()
        {
            var organization = GetOrganizationOne();
            var request = new IntegrationReadListRequest(CurrentUserId, organization.Uid);
            request.PagingInfo.Skip = 0;

            return request;
        }

        public static IntegrationReadListRequest GetIntegrationReadListRequestForSelectMany()
        {
            var organization = GetOrganizationOne();
            var request = new IntegrationReadListRequest(CurrentUserId, organization.Uid);
            request.PagingInfo.Skip = 1;

            return request;
        }

        public static IntegrationRevisionReadListRequest GetIntegrationRevisionReadListRequest()
        {
            var integration = GetOrganizationOneIntegrationOne();
            var request = new IntegrationRevisionReadListRequest(CurrentUserId, integration.Uid);

            return request;
        }

        public static IntegrationReadRequest GetIntegrationReadRequest()
        {
            var integration = GetOrganizationOneIntegrationOne();
            var request = new IntegrationReadRequest(CurrentUserId, integration.Uid);

            return request;
        }

        public static IntegrationEditRequest GetIntegrationEditRequest()
        {
            var integration = GetOrganizationOneIntegrationOne();
            var request = new IntegrationEditRequest(CurrentUserId, integration.Uid, integration.Name, 
                                                    integration.Description);

            return request;
        }
        public static IntegrationDeleteRequest GetIntegrationDeleteRequest()
        {
            var integration = GetOrganizationOneIntegrationOne();
            var request = new IntegrationDeleteRequest(CurrentUserId,integration.Uid);

            return request;
        }
        public static ProjectCreateRequest GetProjectCreateRequest()
        {
            var project = GetOrganizationOneProjectOne();
            var request = new ProjectCreateRequest(CurrentUserId, project.OrganizationUid, project.Name,
                                                   project.Url, project.Description);

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
            var project = GetOrganizationOneProjectOne();
            var request = new ProjectEditRequest(CurrentUserId, project.OrganizationUid, project.Uid,
                                                 project.Name, project.Url, project.Description);

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
            var project = GetOrganizationOneProjectOne();
            var request = new ProjectCloneRequest(CurrentUserId, project.OrganizationUid, project.Uid,
                                                  project.Name, project.Url, project.Description,
                                                  project.LabelCount, project.LabelTranslationCount, project.IsSuperProject);

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
            var organization = GetOrganizationOne();
            var request = new ProjectReadListRequest(CurrentUserId, organization.Uid);

            return request;
        }

        public static ProjectReadRequest GetProjectReadRequest()
        {
            var project = GetOrganizationOneProjectOne();
            var request = new ProjectReadRequest(CurrentUserId, project.Uid);

            return request;
        }

        public static ProjectDeleteRequest GetProjectDeleteRequest()
        {
            var project = GetOrganizationOneProjectOne();
            var request = new ProjectDeleteRequest(CurrentUserId, project.Uid);

            return request;
        }

        public static ProjectChangeActivationRequest GetProjectChangeActivationRequest()
        {
            var project = GetOrganizationOneProjectOne();
            var request = new ProjectChangeActivationRequest(CurrentUserId, project.OrganizationUid, project.Uid);

            return request;
        }

        public static ProjectRestoreRequest GetProjectRestoreRequest()
        {
            var project = GetOrganizationOneProjectOne();
            var request = new ProjectRestoreRequest(CurrentUserId, project.OrganizationUid, One);

            return request;
        }

        public static ProjectRevisionReadListRequest GetProjectRevisionReadListRequest()
        {
            var project = GetOrganizationOneProjectOne();
            var request = new ProjectRevisionReadListRequest(CurrentUserId, project.Uid);

            return request;
        }

        public static ProjectPendingTranslationReadListRequest GetProjectPendingTranslationReadListRequest()
        {
            var project = GetOrganizationOneProjectOne();
            var request = new ProjectPendingTranslationReadListRequest(CurrentUserId, project.Uid);

            return request;
        }

    }
}