using System.Threading.Tasks;

namespace Translation.Common.Contracts
{
    public interface IYandexTranslateProvider
    {
        Task<string> TranslateText(string textToTranslate, string targetLanguageIsoCode2);
    }
}