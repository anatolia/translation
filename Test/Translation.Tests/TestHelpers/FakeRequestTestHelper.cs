using Translation.Common.Models.Requests.Journal;
using Translation.Common.Models.Requests.Project;
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
    }
}