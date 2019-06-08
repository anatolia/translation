using Npgsql;
using StandardRepository;

using Translation.Data.Entities.Domain;

namespace Translation.Data.Repositories.Contracts
{
    public interface IProjectRepository : IStandardRepository<Project, NpgsqlConnection>
    {

    }
}