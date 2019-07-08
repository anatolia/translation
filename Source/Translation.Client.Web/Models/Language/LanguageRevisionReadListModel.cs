using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Language
{
    public class LanguageRevisionReadListModel : BaseModel
    {
        public string LanguageName { get; set; }
        public Guid LanguageUid { get; set; }

        public LanguageRevisionReadListModel()
        {
            Title = "language_revision_list_title";
        }
    }
}