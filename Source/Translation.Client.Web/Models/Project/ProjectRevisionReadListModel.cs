using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Project
{
    public class ProjectRevisionReadListModel : BaseModel
    {
        public string ProjectName { get; set; }
        public Guid ProjectUid { get; set; }

        public ProjectRevisionReadListModel()
        {
            Title = "project_revision_list_title";
        }
    }
}