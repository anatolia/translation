using System.Collections.Generic;
using System.Threading.Tasks;
using StandardRepository.Helpers;
using StandardRepository.PostgreSQL;
using StandardRepository.PostgreSQL.Helpers;
using StandardRepository.PostgreSQL.Helpers.SqlExecutor;

using Translation.Data.Entities.Domain;
using Translation.Data.Repositories.Contracts;

namespace Translation.Data.Repositories
{
    public class ProjectRepository : PostgreSQLRepository<Project>, IProjectRepository
    {
        public ProjectRepository(PostgreSQLTypeLookup typeLookup, PostgreSQLConstants<Project> sqlConstants, EntityUtils entityUtils,
                                 ExpressionUtils expressionUtils, PostgreSQLExecutor sqlExecutor) : base(typeLookup, sqlConstants, entityUtils,
                                                                                                         expressionUtils, sqlExecutor, GetUpdateableFieldsList())
        {

        }

        private static List<string> GetUpdateableFieldsList() => new List<string>
        {
            nameof(Project.Name),
            nameof(Project.Description),
            nameof(Project.Url),
            nameof(Project.IsActive),
        };

        public Task<bool> IsProjectNameMustBeUnique(string name, long organizationId)
        {
            return Any(x => x.Name == name
                            && x.OrganizationId == organizationId);
        }
    }
}