using Translation.Common.Models.Requests.Project;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;
using static Translation.Tests.TestHelpers.GetFakeConstantTestHelper;

namespace Translation.Tests.TestHelpers
{
    public class GetFakeRequestTestHelper
    {
        public static ProjectCreateRequest GetProjectCreateRequest(CurrentOrganization organization, Project project)
        {
            var request = new ProjectCreateRequest(IdCurrentUser, organization.Uid, project.Name,
                                                   project.Url, project.Description);

            return request;
        }

        public static ProjectEditRequest GetProjectEditRequest(Project entity)
        {
            var request = new ProjectEditRequest(IdCurrentUser, entity.OrganizationUid, entity.Uid,
                                                 entity.Name, entity.Url, entity.Description);

            return request;
        }
    }
}