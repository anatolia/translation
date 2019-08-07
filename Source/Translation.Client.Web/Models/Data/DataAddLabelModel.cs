using System;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.Data
{
    public sealed class DataAddLabelModel : BaseModel
    {
        public Guid Token { get; set; }
        public Guid ProjectUid { get; set; }
        public string LabelKey { get; set; }
        public string LanguagesIsoCode2Char { get; set; }
    }
}