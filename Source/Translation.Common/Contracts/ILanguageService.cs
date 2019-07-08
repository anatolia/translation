using System.Threading.Tasks;

using Translation.Common.Models.Requests.Language;
using Translation.Common.Models.Responses.Language;

namespace Translation.Common.Contracts
{
    public interface ILanguageService
    {
        Task<LanguageReadResponse> GetLanguage(LanguageReadRequest request);
        Task<LanguageReadListResponse> GetLanguages(LanguageReadListRequest request);
        Task<LanguageRevisionReadListResponse> GetLanguageRevisions(LanguageRevisionReadListRequest request);
        Task<LanguageCreateResponse> CreateLanguage(LanguageCreateRequest request);
        Task<LanguageEditResponse> EditLanguage(LanguageEditRequest request);
        Task<LanguageDeleteResponse> DeleteLanguage(LanguageDeleteRequest request);
        Task<LanguageRestoreResponse> RestoreLanguage(LanguageRestoreRequest request);
    }
}