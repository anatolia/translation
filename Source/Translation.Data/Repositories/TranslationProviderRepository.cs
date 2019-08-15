using System.Collections.Generic;

using StandardRepository.Helpers;
using StandardRepository.PostgreSQL;
using StandardRepository.PostgreSQL.Helpers;
using StandardRepository.PostgreSQL.Helpers.SqlExecutor;

using Translation.Data.Entities.Domain;
using Translation.Data.Repositories.Contracts;

namespace Translation.Data.Repositories
{
    public class TranslationProviderRepository : PostgreSQLRepository<TranslationProvider>, ITranslationProviderRepository
    {
        public TranslationProviderRepository(PostgreSQLTypeLookup typeLookup, PostgreSQLConstants<TranslationProvider> sqlConstants, EntityUtils entityUtils,
                                 ExpressionUtils expressionUtils, PostgreSQLExecutor sqlExecutor) : base(typeLookup, sqlConstants, entityUtils,
                                                                                                         expressionUtils, sqlExecutor, GetUpdateableFieldsList())
        {

        }

        private static List<string> GetUpdateableFieldsList() => new List<string>
        {
            nameof(TranslationProvider.Name),
            nameof(TranslationProvider.Description),
            nameof(TranslationProvider.IsActive),
        };
    }
}