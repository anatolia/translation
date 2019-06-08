using System.Collections.Generic;

using StandardRepository.Helpers;
using StandardRepository.PostgreSQL;
using StandardRepository.PostgreSQL.Helpers;
using StandardRepository.PostgreSQL.Helpers.SqlExecutor;

using Translation.Data.Entities.Main;
using Translation.Data.Repositories.Contracts;

namespace Translation.Data.Repositories
{
    public class TokenRequestLogRepository : PostgreSQLRepository<TokenRequestLog>, ITokenRequestLogRepository
    {
        public TokenRequestLogRepository(PostgreSQLTypeLookup typeLookup, PostgreSQLConstants<TokenRequestLog> sqlConstants, EntityUtils entityUtils,
                                         ExpressionUtils expressionUtils, PostgreSQLExecutor sqlExecutor) : base(typeLookup, sqlConstants, entityUtils,
                                                                                                                 expressionUtils, sqlExecutor, GetUpdateableFieldsList())
        {

        }

        private static List<string> GetUpdateableFieldsList() => new List<string>
        {

        };
    }
}