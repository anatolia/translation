using System;

namespace Translation.Client.Web.Models.InputModels
{
    public class SelectInputModel : InputModel
    {
        public bool IsOptionTypeContent { get; set; }
        public bool IsAllOptionsVisible { get; set; }
        public bool IsMultiple { get; set; }
        public bool IsHavingDetailInfo { get; set; }
        public bool IsSetFirstItem { get; set; }

        public string DetailInfoDataUrl { get; set; }
        public string DataUrl { get; set; }
        public string Parent { get; set; }

        public string Text { get; set; }
        public string TextFieldName { get; set; }

        public bool IsAddNewEnabled { get; set; }
        public string AddNewUrl { get; set; }

        public SelectInputModel(string name, string labelKey, string dataUrl, bool required = false, string addNewUrl = "") : base(name + "Uid", labelKey, required)
        {
            TextFieldName = $"{name}Name";
            DataUrl = dataUrl;
            AddNewUrl = addNewUrl;
            IsAddNewEnabled = !string.IsNullOrEmpty(addNewUrl);
            IsSetFirstItem = true;
        }
    }
}