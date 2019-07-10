using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Project
{
    public class ProjectPendingTranslationReadListModel : BaseModel
    {
        public Guid ProjectUid { get; set; }
        public string ProjectName { get; set; }

        public ProjectPendingTranslationReadListModel()
        {
            Title = "project_pending_translations_title";
        }
    }
}