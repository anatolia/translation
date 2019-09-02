using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using StandardRepository.Helpers;
using StandardRepository.PostgreSQL;
using StandardRepository.PostgreSQL.Helpers;
using StandardRepository.PostgreSQL.Helpers.SqlExecutor;

using Translation.Data.Entities.Main;
using Translation.Data.Repositories.Contracts;

namespace Translation.Data.Repositories
{
    public class OrganizationRepository : PostgreSQLRepository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(PostgreSQLTypeLookup typeLookup, PostgreSQLConstants<Organization> sqlConstants, EntityUtils entityUtils,
                                      ExpressionUtils expressionUtils, PostgreSQLExecutor sqlExecutor) : base(typeLookup, sqlConstants, entityUtils,
                                                                                                              expressionUtils, sqlExecutor, GetUpdateableFieldsList())
        {

        }

        private static List<string> GetUpdateableFieldsList() => new List<string>
        {
            nameof(Organization.Name),
            nameof(Organization.Description),
            nameof(Organization.IsActive)
        };

        public async Task<bool> IsOrganizationActive(long organizationId)
        {
            return await Any(x => x.Id == organizationId && !x.IsActive);
        }
    }
}