using System.Collections.Generic;

using StandardRepository.Helpers;
using StandardRepository.PostgreSQL;
using StandardRepository.PostgreSQL.Helpers;
using StandardRepository.PostgreSQL.Helpers.SqlExecutor;

using Translation.Data.Entities.Parameter;
using Translation.Data.Repositories.Contracts;

namespace Translation.Data.Repositories
{
    public class LanguageRepository : PostgreSQLRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(PostgreSQLTypeLookup typeLookup, PostgreSQLConstants<Language> sqlConstants, EntityUtils entityUtils,
                                  ExpressionUtils expressionUtils, PostgreSQLExecutor sqlExecutor) : base(typeLookup, sqlConstants, entityUtils,
                                                                                                          expressionUtils, sqlExecutor, GetUpdateableFieldsList())
        {

        }

        private static List<string> GetUpdateableFieldsList() => new List<string>
        {
            nameof(Language.Name),
            nameof(Language.Description),
            nameof(Language.IsoCode2Char),
            nameof(Language.IsoCode3Char),
            nameof(Language.IconUrl)
        };
    }
}