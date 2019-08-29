using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Npgsql;
using StandardRepository;

using Translation.Data.Entities.Domain;

namespace Translation.Data.Repositories.Contracts
{
    public interface IProjectRepository : IStandardRepository<Project, NpgsqlConnection>
    {
        Task<bool> IsProjectNameMustBeUnique(string name, long organizationId);
    }
}