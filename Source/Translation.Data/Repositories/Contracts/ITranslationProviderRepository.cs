using Npgsql;
using StandardRepository;

using Translation.Data.Entities.Domain;

namespace Translation.Data.Repositories.Contracts
{
    public interface ITranslationProviderRepository : IStandardRepository<TranslationProvider, NpgsqlConnection>
    {

    }
}