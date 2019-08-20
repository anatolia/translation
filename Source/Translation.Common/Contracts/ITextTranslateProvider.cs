using System.Threading.Tasks;

namespace Translation.Common.Contracts
{
    public interface ITextTranslateProvider
    {
        Task<string> TranslateText(string textToTranslate, string targetLanguageIsoCode2, string sourceLanguageIsoCode2);
        string Name { get; set; }
    }
}