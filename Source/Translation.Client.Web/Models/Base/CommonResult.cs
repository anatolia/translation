using System.Collections.Generic;

namespace Translation.Client.Web.Models.Base
{
    public class CommonResult
    {
        public bool IsOk { get; set; }
        public List<string> Messages { get; set; }
        public object Item { get; set; }

        public CommonResult()
        {
            Messages = new List<string>();
        }
    }
}