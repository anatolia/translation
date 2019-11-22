using System.Collections.Generic;

using StandardUtils.Models.Shared;

namespace Translation.Client.Web.Models.Base
{
    public class DataResult
    {
        public const string SEPARATOR = ",_,";

        public List<DataHeaderInfo> Headers { get; set; }
        public List<string> Data { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public DataResult()
        {
            Headers = new List<DataHeaderInfo>();
            Data = new List<string>();
            PagingInfo = new PagingInfo();
        }

        public void AddHeaders(params string[] name)
        {
            for (var i = 0; i < name.Length; i++)
            {
                var item = name[i];
                Headers.Add(new DataHeaderInfo { Key = item, DisplayIndex = i });
            }
        }

        public string PrepareImage(string srcAttribute, string altAttribute = "")
        {
            return $"<img src='{srcAttribute}' alt='{altAttribute}' title='{altAttribute}' />";
        }

        public string PrepareLink(string url, string name, bool isTargetBlank = false)
        {
            var targetBlank = string.Empty;
            if (isTargetBlank)
            {
                targetBlank = " target='_blank'";
            }

            return $"<a href='{url}'{targetBlank}>{name}</a>";
        }

        public string PrepareLink(string url, bool isTargetBlank = true)
        {
            return PrepareLink(url, url, isTargetBlank);
        }

        public string PrepareButton(string name, string onClick, string className, string confirmTitle, string confirmContent)
        {
            return $"<button type='button' data-confirm-title='{confirmTitle}' data-confirm-content='{confirmContent}' class='btn {className}' onclick='{onClick}' data-translation='{name}'>{name}</button>";
        }

        /// <summary>
        /// Gets uid from row attribute at client side.
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="name"></param>
        /// <param name="confirmTitle"></param>
        /// <param name="confirmContent"></param>
        /// <returns></returns>
        public string PrepareDeleteButton(string postUrl, string name = "delete", 
                                          string confirmTitle = "are_you_sure_you_want_to_delete_title", 
                                          string confirmContent = "are_you_sure_you_want_to_delete_content")
        {
            return PrepareButton(name, $"handleDeleteRow(this, \"{postUrl}\")", "btn-delete", confirmTitle, confirmContent);
        }

        /// <summary>
        /// Gets uid from row attribute at client side.
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="name"></param>
        /// <param name="confirmTitle"></param>
        /// <param name="confirmContent"></param>
        /// <returns></returns>
        public string PrepareChangeActivationButton(string postUrl, string name = "change_activation", 
                                                    string confirmTitle = "are_you_sure_you_want_to_change_activation_title", 
                                                    string confirmContent = "are_you_sure_you_want_to_change_activation_content")
        {
            return PrepareButton(name, $"handleChangeActivationRow(this, \"{postUrl}\")", "btn-secondary", confirmTitle, confirmContent);
        }

        public string PrepareChangeAllActivationButton(string postUrl, string name = "change_all_activation",
            string confirmTitle = "are_you_sure_you_want_to_change_activation_title",
            string confirmContent = "are_you_sure_you_want_to_change_activation_content")
        {
            return PrepareButton(name, $"handleChangeActivationAllRow(this, \"{postUrl}\")", "btn-secondary", confirmTitle, confirmContent);
        }

        public string PrepareRestoreButton(string name, string postUrl, string redirectUrl, string confirmTitle = "are_you_sure_you_want_to_restore_title", string confirmContent = "are_you_sure_you_want_to_restore_content")
        {
            return PrepareButton(name, $"handleRestoreRow(this, \"{postUrl}\", \"{redirectUrl}\")", "btn-secondary", confirmTitle, confirmContent);
        }
    }
}