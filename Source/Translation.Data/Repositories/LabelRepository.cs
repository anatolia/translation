using System.Collections.Generic;

using StandardRepository.Helpers;
using StandardRepository.PostgreSQL;
using StandardRepository.PostgreSQL.Helpers;
using StandardRepository.PostgreSQL.Helpers.SqlExecutor;

using Translation.Data.Entities.Domain;
using Translation.Data.Repositories.Contracts;

namespace Translation.Data.Repositories
{
    public class LabelRepository : PostgreSQLRepository<Label>, ILabelRepository
    {
        public LabelRepository(PostgreSQLTypeLookup typeLookup, PostgreSQLConstants<Label> sqlConstants, EntityUtils entityUtils,
                               ExpressionUtils expressionUtils, PostgreSQLExecutor sqlExecutor) : base(typeLookup, sqlConstants, entityUtils,
                                                                                                       expressionUtils, sqlExecutor, GetUpdateableFieldsList())
        {

        }

        private static List<string> GetUpdateableFieldsList() => new List<string>
        {
            nameof(Label.LabelKey),
            nameof(Label.Description),
            nameof(Label.IsActive)
        };
    }
}