using System;
using Microsoft.AspNetCore.Http;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Language
{
    public sealed class LanguageDetailModel : BaseModel
    {
        public Guid LanguageUid { get; set; }

        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string IsoCode2 { get; set; }
        public string IsoCode3 { get; set; }
        public IFormFile Icon { get; set; }
        public string Description { get; set; }

        public LanguageDetailModel()
        {
            Title = "language_detail_title";
        }

        public override void SetInputModelValues()
        {
        }
    }
}
