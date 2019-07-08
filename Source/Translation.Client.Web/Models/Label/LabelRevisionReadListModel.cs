using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Label
{
    public class LabelRevisionReadListModel : BaseModel
    {
        public string LabelName { get; set; }
        public Guid LabelUid { get; set; }

        public LabelRevisionReadListModel()
        {
            Title = "label_revision_list_title";
        }
    }
}