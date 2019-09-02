using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Npgsql;
using StandardRepository;

using Translation.Data.Entities.Main;

namespace Translation.Data.Repositories.Contracts
{
    public interface IOrganizationRepository : IStandardRepository<Organization, NpgsqlConnection>
    {
        Task<bool> IsOrganizationActive(long organizationId);
    }
}