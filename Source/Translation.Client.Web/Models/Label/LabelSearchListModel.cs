using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Label
{
    public sealed class LabelSearchListModel : BaseModel
    {
        public string SearchTerm { get; set; }

        public LabelSearchListModel()
        {
            Title = "label_search_list_title";
        }
    }
}