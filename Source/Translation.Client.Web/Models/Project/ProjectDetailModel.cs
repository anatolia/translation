using System;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.Project
{
    public sealed class ProjectDetailModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public Guid ProjectUid { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string OrganizationName { get; set; }
        public string Url { get; set; }
        public int LabelCount { get; set; }
        public bool IsActive { get; set; }

        public HiddenInputModel OrganizationUidInput { get; }
        public HiddenInputModel ProjectUidInput { get; }
        public CheckboxInputModel IsActiveInput { get; set; }

        public ProjectDetailModel()
        {
            Title = "project_detail_title";

            OrganizationUidInput = new HiddenInputModel("OrganizationUid");
            ProjectUidInput = new HiddenInputModel("ProjectUid");
            IsActiveInput = new CheckboxInputModel("IsActive", "is_active", false, true);
        }

        public override void SetInputModelValues()
        {
            OrganizationUidInput.Value = OrganizationUid.ToUidString();
            ProjectUidInput.Value = ProjectUid.ToUidString();
        }
    }
}