using System;

using StandardUtils.Helpers;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;

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

        public HiddenInputModel OrganizationInput { get; }
        public HiddenInputModel ProjectInput { get; }
        public HiddenInputModel LabelInput { get; }
        public CheckboxInputModel IsActiveInput { get; set; }

        public LabelDetailModel()
        {
            Title = "label_detail_title";

            OrganizationInput = new HiddenInputModel("OrganizationUid");
            ProjectInput = new HiddenInputModel("ProjectUid");
            LabelInput = new HiddenInputModel("LabelUid");
            IsActiveInput = new CheckboxInputModel("IsActive", "is_active", true, true);
        }

        public override void SetInputModelValues()
        {
            OrganizationInput.Value = OrganizationUid.ToUidString();
            ProjectInput.Value = ProjectUid.ToUidString();
            LabelInput.Value = LabelUid.ToUidString();
        }
    }
}