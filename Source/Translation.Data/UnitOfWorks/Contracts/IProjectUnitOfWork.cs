using System.Threading.Tasks;

using Translation.Data.Entities.Domain;

namespace Translation.Data.UnitOfWorks.Contracts
{
    public interface IProjectUnitOfWork
    {
        Task<bool> DoCreateWork(long currentUserId, Project project);
        Task<bool> DoDeleteWork(long currentUserId, Project project);

        Task<bool> DoCloneWork(long currentUserId, long projectId, Project newProject);
    }
}