using System.Collections.Generic;

using StandardRepository.Helpers;
using StandardRepository.PostgreSQL;
using StandardRepository.PostgreSQL.Helpers;
using StandardRepository.PostgreSQL.Helpers.SqlExecutor;

using Translation.Data.Entities.Domain;
using Translation.Data.Repositories.Contracts;

namespace Translation.Data.Repositories
{
    public class LabelTranslationRepository : PostgreSQLRepository<LabelTranslation>, ILabelTranslationRepository
    {
        public LabelTranslationRepository(PostgreSQLTypeLookup typeLookup, PostgreSQLConstants<LabelTranslation> sqlConstants, EntityUtils entityUtils,
                                          ExpressionUtils expressionUtils, PostgreSQLExecutor sqlExecutor) : base(typeLookup, sqlConstants, entityUtils,
                                                                                                                  expressionUtils, sqlExecutor, GetUpdateableFieldsList())
        {

        }

        private static List<string> GetUpdateableFieldsList() => new List<string>
        {
            nameof(LabelTranslation.TranslationText),
            nameof(LabelTranslation.Description)
        };
    }
}