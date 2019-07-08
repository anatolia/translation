using System;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.Project
{
    public class ProjectCloneModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public Guid CloningProjectUid { get; set; }
        public string Name { get; set; }

        public string Url { get; set; }
        public string Description { get; set; }

        public int LabelCount { get; set; }
        public int LabelTranslationCount { get; set; }
        public bool IsSuperProject { get; set; }

        public HiddenInputModel OrganizationUidInput { get; }
        public HiddenInputModel CloningProjectUidInput { get; }

        public InputModel NameInput { get; }
        public UrlInputModel UrlInput { get; }
        public LongInputModel DescriptionInput { get; }

        public HiddenInputModel LabelCountInput { get; set; }
        public HiddenInputModel LabelTranslationCountInput { get; set; }
        public CheckboxInputModel IsSuperProjectInput { get; set; }

        public ProjectCloneModel()
        {
            Title = "project_clone_title";

            OrganizationUidInput = new HiddenInputModel("OrganizationUid");
            CloningProjectUidInput = new HiddenInputModel("CloningProjectUid");
            NameInput = new InputModel("Name", "name");

            UrlInput = new UrlInputModel("Url", "url");
            DescriptionInput = new LongInputModel("Description", "description");

            LabelCountInput = new HiddenInputModel("LabelCount");
            LabelTranslationCountInput = new HiddenInputModel("LabelTranslationCount");
            IsSuperProjectInput = new CheckboxInputModel("IsSuperProject", "is_super_project");
        }

        public override void SetInputModelValues()
        {
            OrganizationUidInput.Value = OrganizationUid.ToUidString();
            CloningProjectUidInput.Value = CloningProjectUid.ToUidString();
            NameInput.Value = Name;

            UrlInput.Value = Url;
            DescriptionInput.Value = Description;

            LabelCountInput.Value = LabelCount.ToString();
            LabelTranslationCountInput.Value = LabelTranslationCount.ToString();
            IsSuperProjectInput.Value = IsSuperProject;
        }

        public override void SetInputErrorMessages()
        {
            if (OrganizationUid.IsEmptyGuid())
            {
                ErrorMessages.Add("organization_uid_is_not_valid");
            }

            if (CloningProjectUid.IsEmptyGuid())
            {
                ErrorMessages.Add("cloning_project_uid_is_not_valid");
            }

            Name = Name.TrimOrDefault();
            if (Name.IsEmpty())
            {
                NameInput.ErrorMessage.Add("project_name_required_error_message");
                InputErrorMessages.AddRange(NameInput.ErrorMessage);
            }

            Url = Url.TrimOrDefault();
            if (Url.IsNotEmpty()
                && Url.IsNotUrl())
            {
                UrlInput.ErrorMessage.Add("url_is_not_valid_error_message");
                InputErrorMessages.AddRange(UrlInput.ErrorMessage);
            }
        }
    }
}