using System;

using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Models.Label
{
    public class CreateBulkLabelDoneModel : BaseModel
    {
        public Guid ProjectUid { get; set; }
        public string ProjectName{ get; set; }

        public int AddedLabelCount { get; set; }
        public int CanNotAddedLabelCount { get; set; }
        
        public int CanNotAddedLabelTranslationCount { get; set; }
        public int AddedLabelTranslationCount { get; set; }
        public int TotalRowsProcessed { get; set; }

        public CreateBulkLabelDoneModel()
        {
            Title = "create_bulk_label_done_title";
        }
    }
}