using System.Collections.Generic;

namespace Translation.Integrations
{
    public class YandexTranslateResponse
    {
        public int Code { get; set; }
        public string Lang { get; set; }
        public List<string> Text { get; set; }
    }
}