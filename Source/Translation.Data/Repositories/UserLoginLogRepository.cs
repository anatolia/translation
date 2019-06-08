using System.Collections.Generic;

using StandardRepository.Helpers;
using StandardRepository.PostgreSQL;
using StandardRepository.PostgreSQL.Helpers;
using StandardRepository.PostgreSQL.Helpers.SqlExecutor;

using Translation.Data.Entities.Main;
using Translation.Data.Repositories.Contracts;

namespace Translation.Data.Repositories
{
    public class UserLoginLogRepository : PostgreSQLRepository<UserLoginLog>, IUserLoginLogRepository
    {
        public UserLoginLogRepository(PostgreSQLTypeLookup typeLookup, PostgreSQLConstants<UserLoginLog> sqlConstants, EntityUtils entityUtils,
                                      ExpressionUtils expressionUtils, PostgreSQLExecutor sqlExecutor) : base(typeLookup, sqlConstants, entityUtils,
                                                                                                              expressionUtils, sqlExecutor, GetUpdateableFieldsList())
        {

        }

        private static List<string> GetUpdateableFieldsList() => new List<string>
        {
        };
    }
}