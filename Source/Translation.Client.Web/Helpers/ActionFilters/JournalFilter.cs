using Microsoft.AspNetCore.Mvc.Filters;

using Translation.Common.Exceptions;
using Translation.Common.Helpers;
using Translation.Common.Models.Requests.Journal;

namespace Translation.Client.Web.Helpers.ActionFilters
{
    public class JournalFilter : ActionFilterAttribute
    {
        public string Message { get; set; }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var controller = context.Controller as IJournalingController;
            if (controller == null)
            {
                return;
            }

            if (Message.IsEmpty())
            {
                return;
            }

            if (!controller.CurrentUser.IsActionSucceed)
            {
                return;
            }

            var request = new JournalCreateRequest(controller.CurrentUser.Id, Message);
            var response = controller.JournalService.CreateJournal(request);
            if (response.Status.IsNotSuccess)
            {
                throw new JournalException("couldn't write to journal > " + Message);
            }

            base.OnActionExecuted(context);
        }
    }
}