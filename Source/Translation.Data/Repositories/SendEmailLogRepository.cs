using System.Collections.Generic;

using StandardRepository.Helpers;
using StandardRepository.PostgreSQL;
using StandardRepository.PostgreSQL.Helpers;
using StandardRepository.PostgreSQL.Helpers.SqlExecutor;

using Translation.Data.Entities.Main;
using Translation.Data.Repositories.Contracts;

namespace Translation.Data.Repositories
{
    public class SendEmailLogRepository : PostgreSQLRepository<SendEmailLog>, ISendEmailLogRepository
    {
        public SendEmailLogRepository(PostgreSQLTypeLookup typeLookup, PostgreSQLConstants<SendEmailLog> sqlConstants, EntityUtils entityUtils,
                                      ExpressionUtils expressionUtils, PostgreSQLExecutor sqlExecutor) : base(typeLookup, sqlConstants, entityUtils,
                                                                                                              expressionUtils, sqlExecutor, GetUpdateableFieldsList())
        {

        }

        private static List<string> GetUpdateableFieldsList() => new List<string>
        {

        };
    }
}