using System;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;
using Translation.Common.Helpers;

namespace Translation.Client.Web.Models.Label
{
    public sealed class LabelCloneModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public Guid CloningLabelUid { get; set; }
        public string CloningLabelKey { get; set; }
        public string CloningLabelDescription { get; set; }

        public Guid ProjectUid { get; set; }
        public string Key { get; set; }
        public string Description { get; set; }

        public int CloningLabelTranslationCount { get; set; }

        public HiddenInputModel OrganizationUidInput { get; }
        public HiddenInputModel CloningLabelUidInput { get; }
        public HiddenInputModel CloningLabelKeyInput { get; }

        public SelectInputModel ProjectUidInput { get; }
        public InputModel KeyInput { get; }
        public LongInputModel DescriptionInput { get; }

        public HiddenInputModel CloningLabelTranslationCountInput { get; }

        public LabelCloneModel()
        {
            Title = "label_clone_title";

            OrganizationUidInput = new HiddenInputModel("OrganizationUid");
            CloningLabelUidInput = new HiddenInputModel("CloningLabelUid");
            CloningLabelKeyInput = new HiddenInputModel("CloningLabelKey");

            ProjectUidInput = new SelectInputModel("ProjectUid", "ProjectName", "project", "/Project/SelectData/");
            KeyInput = new InputModel("Key", "key");
            DescriptionInput = new LongInputModel("Description", "description");

            CloningLabelTranslationCountInput = new HiddenInputModel("CloningLabelTranslationCount", "cloning_label_translation_count");
        }

        public override void SetInputModelValues()
        {
            OrganizationUidInput.Value = OrganizationUid.ToUidString();
            CloningLabelUidInput.Value = CloningLabelUid.ToUidString();
            CloningLabelKeyInput.Value = CloningLabelKey;
            ProjectUidInput.Value = ProjectUid.ToUidString();

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

            if (ProjectUid.IsEmptyGuid())
            {
                ProjectUidInput.ErrorMessage.Add("project_required_error_message");
                InputErrorMessages.AddRange(ProjectUidInput.ErrorMessage);
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