using Npgsql;
using StandardRepository;

using Translation.Data.Entities.Parameter;

namespace Translation.Data.Repositories.Contracts
{
    public interface ILanguageRepository : IStandardRepository<Language, NpgsqlConnection>
    {
        
    }
}