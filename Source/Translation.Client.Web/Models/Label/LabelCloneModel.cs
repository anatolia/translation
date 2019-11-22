using System;

using StandardUtils.Helpers;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;

namespace Translation.Client.Web.Models.Label
{
    public sealed class LabelCloneModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public Guid CloningLabelUid { get; set; }
        public string CloningLabelKey { get; set; }
        public string CloningLabelDescription { get; set; }

        public Guid Project { get; set; }
        public string ProjectName { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }

        public int CloningLabelTranslationCount { get; set; }

        public HiddenInputModel OrganizationInput { get; }
        public HiddenInputModel CloningLabelInput { get; }
        public HiddenInputModel CloningLabelKeyInput { get; }

        public SelectInputModel ProjectInput { get; }
        public InputModel KeyInput { get; }
        public LongInputModel DescriptionInput { get; }

        public HiddenInputModel CloningLabelTranslationCountInput { get; }

        public LabelCloneModel()
        {
            Title = "label_clone_title";

            OrganizationInput = new HiddenInputModel("OrganizationUid");
            CloningLabelInput = new HiddenInputModel("CloningLabelUid");
            CloningLabelKeyInput = new HiddenInputModel("CloningLabelKey");

            ProjectInput = new SelectInputModel("Project", "project", "/Project/SelectData/");
            KeyInput = new InputModel("Key", "key");
            DescriptionInput = new LongInputModel("Description", "description");

            CloningLabelTranslationCountInput = new HiddenInputModel("CloningLabelTranslationCount", "cloning_label_translation_count");
        }

        public override void SetInputModelValues()
        {
            OrganizationInput.Value = OrganizationUid.ToUidString();
            CloningLabelInput.Value = CloningLabelUid.ToUidString();
            CloningLabelKeyInput.Value = CloningLabelKey;
            ProjectInput.Value = Project.ToUidString();

            KeyInput.Value = CloningLabelKey;
            DescriptionInput.Value = CloningLabelDescription;

            CloningLabelTranslationCountInput.Value = CloningLabelTranslationCount.ToString();
        }

        public override void SetInputErrorMessages()
        {
            if (OrganizationUid.IsEmptyGuid())
            {
                ErrorMessages.Add("organization_uid_not_valid");
            }

            if (CloningLabelUid.IsEmptyGuid())
            {
                ErrorMessages.Add("cloning_label_uid_not_valid");
            }

            if (CloningLabelKey.IsEmpty())
            {
                ErrorMessages.Add("cloning_label_key_required");
            }

            if (Project.IsEmptyGuid())
            {
                ProjectInput.ErrorMessage.Add("project_required_error_message");
                InputErrorMessages.AddRange(ProjectInput.ErrorMessage);
            }

            Key = Key.TrimOrDefault();
            if (Key.IsEmpty())
            {
                KeyInput.ErrorMessage.Add("key_required_error_message");
                InputErrorMessages.AddRange(KeyInput.ErrorMessage);
            }
        }
    }
}