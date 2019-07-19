using System.Threading.Tasks;

using Translation.Common.Models.Requests.Project;
using Translation.Common.Models.Responses.Project;

namespace Translation.Common.Contracts
{
    public interface IProjectService
    {
        Task<ProjectReadListResponse> GetProjects(ProjectReadListRequest request);
        Task<ProjectRevisionReadListResponse> GetProjectRevisions(ProjectRevisionReadListRequest request);
        Task<ProjectReadResponse> GetProject(ProjectReadRequest request);
        Task<ProjectReadBySlugResponse> GetProjectBySlug(ProjectReadBySlugRequest request);
        Task<ProjectCreateResponse> CreateProject(ProjectCreateRequest request);
        Task<ProjectEditResponse> EditProject(ProjectEditRequest request);
        Task<ProjectDeleteResponse> DeleteProject(ProjectDeleteRequest request);
        Task<ProjectCloneResponse> CloneProject(ProjectCloneRequest request);
        Task<ProjectChangeActivationResponse> ChangeActivationForProject(ProjectChangeActivationRequest request);
        Task<ProjectRestoreResponse> RestoreProject(ProjectRestoreRequest request);
        Task<ProjectPendingTranslationReadListResponse> GetPendingTranslations(ProjectPendingTranslationReadListRequest request);
    }
}