using System;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.Label
{
    public sealed class LabelDetailModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public Guid ProjectUid { get; set; }
        public string ProjectName { get; set; }

        public Guid LabelUid { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public int LabelTranslationCount { get; set; }

        public HiddenInputModel OrganizationUidInput { get; }
        public HiddenInputModel ProjectUidInput { get; }
        public HiddenInputModel LabelUidInput { get; }
        public CheckboxInputModel IsActiveInput { get; set; }

        public LabelDetailModel()
        {
            Title = "label_detail_title";

            OrganizationUidInput = new HiddenInputModel("OrganizationUid");
            ProjectUidInput = new HiddenInputModel("ProjectUid");
            LabelUidInput = new HiddenInputModel("LabelUid");
            IsActiveInput = new CheckboxInputModel("IsActive", "is_active", true, true);
        }

        public override void SetInputModelValues()
        {
            OrganizationUidInput.Value = OrganizationUid.ToUidString();
            ProjectUidInput.Value = ProjectUid.ToUidString();
            LabelUidInput.Value = LabelUid.ToUidString();
        }
    }
}