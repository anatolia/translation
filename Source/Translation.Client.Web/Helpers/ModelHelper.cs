using Translation.Client.Web.Models;
using Translation.Client.Web.Models.Base;
using Translation.Common.Models.Base;

namespace Translation.Client.Web.Helpers
{
    public static class ModelHelper
    {
        public static void MapMessages(this BaseModel model, BaseResponse response)
        {
            model.ErrorMessages.AddRange(response.ErrorMessages);
            model.InfoMessages.AddRange(response.InfoMessages);
            model.WarningMessages.AddRange(response.WarningMessages);
            model.SuccessMessages.AddRange(response.SuccessMessages);

            model.SetInputModelValues();
        }
    }
}