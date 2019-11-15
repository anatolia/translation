using StandardUtils.Models.Responses;
using Translation.Client.Web.Models.Base;

namespace Translation.Client.Web.Helpers
{
    public static class ModelHelper
    {
        public static void MapMessages(this BaseModel model, BaseResponse response)
        {
            model.ErrorMessages.AddRange(response.ErrorMessages);

            model.SetInputModelValues();
        }
    }
}