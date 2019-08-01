using System.Threading.Tasks;

namespace Translation.Common.Contracts
{
    public interface IGoogleTranslateProvider
    {
        Task<string> TranslateText(string textToTranslate, string targetLanguageIsoCode2, string sourceLanguageIsoCode2);
    }
}