using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Shouldly;

using StandardUtils.Models.Shared;

using Translation.Client.Web.Controllers;
using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;

using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.TestHelpers
{
    public class AssertViewModelTestHelper
    {
        public static void AssertInputErrorMessagesOfView<T>(IActionResult result, T model) where T : BaseModel
        {
            AssertViewWithModel<T>(result);

            model.InputErrorMessages.Clear();
            model.IsValid();

            var messages = ((T)((ViewResult)result).Model).InputErrorMessages;
            messages.Count.ShouldBe(model.InputErrorMessages.Count);

            for (var i = 0; i < messages.Count; i++)
            {
                var message = messages[i];
                model.InputErrorMessages.ShouldContain(message);
            }
        }

        public static void AssertErrorMessagesForInvalidOrFailedResponse<T>(IActionResult result) where T : BaseModel
        {
            AssertViewWithModel<T>(result);

            var messages = ((T)((ViewResult)result).Model).ErrorMessages;
            messages.Any(x => x == StringOne).ShouldBeTrue();
        }

        public static void AssertErrorMessagesForInvalidOrFailedResponse<T>(string errorMessage, IActionResult result) where T : BaseModel
        {
            AssertViewWithModel<T>(result);

            var messages = ((T)((ViewResult)result).Model).ErrorMessages;
            messages.Any(x => x == errorMessage).ShouldBeTrue();
        }

        public static void AssertViewWithModel<T>(IActionResult result)
        {
            result.ShouldNotBeNull();
            var viewResult = ((ViewResult)result);
            viewResult.ViewName.ShouldBeNull();
            viewResult.Model.ShouldNotBeNull();
            viewResult.Model.ShouldBeAssignableTo<T>();
        }

        public static void AssertViewWithModelAndMessage<T>(string viewName, IActionResult result)
        {
            result.ShouldNotBeNull();
            var viewResult = ((ViewResult)result);
            viewResult.ViewName.ShouldNotBeNull();
            viewResult.Model.ShouldNotBeNull();
            viewResult.Model.ShouldBeAssignableTo<T>();
            viewResult.ViewName.ShouldBe(viewName);
        }

        public static void AssertView<T>(ViewResult result)
        {
            result.ShouldNotBeNull();
            result.ViewName.ShouldBeNull();
            result.Model.ShouldNotBeNull();
            result.Model.ShouldBeAssignableTo<T>();
        }

        public static void AssertView<T>(T result)
        {
            result.ShouldNotBeNull();
            result.ShouldBeAssignableTo<T>();
        }

        public static void AssertView<T>(Task<IActionResult> result)
        {
            result.ShouldNotBeNull();
        }

        public static void AssertView<T>(IActionResult result)
        {
            result.ShouldNotBeNull();
        }

        public static void AssertViewAndHeaders(Task<IActionResult> result, string[] headers)
        {
            result.ShouldNotBeNull();
            var jsonResult = result.Result as JsonResult;
            jsonResult.ShouldNotBeNull();
            var dataResult = jsonResult.Value as DataResult;
            dataResult.ShouldNotBeNull();
            headers.Length.ShouldBe(dataResult.Headers.Count);

            for (var i = 0; i < dataResult.Headers.Count; i++)
            {
                headers[i].ShouldBe(dataResult.Headers[i].Key);
            }
        }

        public static void AssertView<T>(Task<RedirectResult> result)
        {
            result.ShouldNotBeNull();
        }

        public static void AssertView<T>(JsonResult result)
        {
            result.ShouldNotBeNull();
            result.Value.ShouldNotBeNull();
            result.Value.ShouldBeAssignableTo<T>();
        }

        public static void AssertView<T>(FileResult result)
        {
            result.ShouldNotBeNull();
            result.ContentType.ShouldNotBeNull();
            result.FileDownloadName.ShouldNotBeNull();
        }

        public static void AssertView<T>(RedirectResult result)
        {
            result.ShouldNotBeNull();
        }

        public static void AssertViewAccessDenied(IActionResult result)
        {
            var controller = new BaseController(null, null, null, null);
            var redirectAccessDenied = controller.RedirectToAccessDenied();

            result.ShouldNotBeNull();
            ((RedirectResult)result).Url.ShouldBe(redirectAccessDenied.Url);
        }

        public static void AssertViewRedirectToHome(IActionResult result)
        {
            var controller = new BaseController(null, null, null, null);
            var redirectToHome = controller.RedirectToHome();

            result.ShouldNotBeNull();
            ((RedirectResult)result).Url.ShouldBe(redirectToHome.Url);
        }

        public static void AssertView<T>(RedirectToActionResult result, string actionName)
        {
            result.ShouldNotBeNull();
            result.ActionName.ShouldBe(actionName);
        }

        public static void AssertPagingInfo(JsonResult result)
        {
            var pagingInfo = new PagingInfo();

            var resultValue = (DataResult)result.Value;
            resultValue.PagingInfo.PagingType.ShouldBe(PagingInfo.PAGE_NUMBERS);
            resultValue.PagingInfo.Take.ShouldBe(pagingInfo.Take);
            resultValue.PagingInfo.Skip.ShouldBe(pagingInfo.Skip);
        }

        public static void AssertHiddenInputModel(HiddenInputModel input, string name, string value = "")
        {
            input.ShouldNotBeNull();
            input.Name.ShouldBe(name);
            input.Value.ShouldBe(value);
        }

        public static void AssertInputModel(InputModel input, string name, string labelKey)
        {
            input.ShouldNotBeNull();
            input.Name.ShouldBe(name);
            input.LabelKey.ShouldBe(labelKey);
        }

        public static void AssertInputModel(InputModel input, string name, string labelKey, bool isRequired)
        {
            input.ShouldNotBeNull();
            input.Name.ShouldBe(name);
            input.LabelKey.ShouldBe(labelKey);
            input.IsRequired.ShouldBe(isRequired);
        }

        public static void AssertShortInputModel(ShortInputModel input, string name, string labelKey,
                                                 bool isRequired = false, string value = "")
        {
            input.ShouldNotBeNull();
            input.Name.ShouldBe(name);
            input.LabelKey.ShouldBe(labelKey);
            input.IsRequired.ShouldBe(isRequired);
            input.Value.ShouldBe(value);
        }

        public static void AssertLongInputModel(LongInputModel input, string name, string labelKey,
                                                bool isRequired = false, string value = "")
        {
            input.ShouldNotBeNull();
            input.Name.ShouldBe(name);
            input.LabelKey.ShouldBe(labelKey);
            input.IsRequired.ShouldBe(isRequired);
            input.Value.ShouldBe(value);
        }

        public static void AssertSelectInputModel(SelectInputModel input, string name, string labelKey, string dataUrl, bool required = false, string addNewUrl = "")
        {
            input.ShouldNotBeNull();
            input.Name.ShouldBe(name + "Uid");
            input.LabelKey.ShouldBe(labelKey);
            input.DataUrl.ShouldBe(dataUrl);
            input.IsRequired.ShouldBe(required);
            input.AddNewUrl.ShouldBe(addNewUrl);
        }

        public static void AssertCheckboxInputModel(CheckboxInputModel input, string name, string labelKey)
        {
            input.ShouldNotBeNull();
            input.Name.ShouldBe(name);
            input.LabelKey.ShouldBe(labelKey);
        }

        public static void AssertCheckboxInputModel(CheckboxInputModel input, string name, string labelKey, bool isRequired, bool isReadOnly)
        {
            input.ShouldNotBeNull();
            input.Name.ShouldBe(name);
            input.LabelKey.ShouldBe(labelKey);
            input.IsRequired.ShouldBe(isRequired);
            input.IsReadOnly.ShouldBe(isReadOnly);
        }

        public static void AssertFileInputModel(FileInputModel input, string name, string labelKey, bool isRequired = false, bool isMultiple = false)
        {
            input.ShouldNotBeNull();
            input.Name.ShouldBe(name);
            input.LabelKey.ShouldBe(labelKey);
            input.IsRequired.ShouldBe(isRequired);
            input.IsMultiple.ShouldBe(isMultiple);
        }

        public static void AssertMessages(List<string> modelErrorMessages, string[] errorMessages)
        {
            modelErrorMessages.ShouldNotBeNull();

            if (errorMessages == null)
            {
                modelErrorMessages.Count.ShouldBe(0);
            }
            else
            {
                for (int i = 0; i < modelErrorMessages.Count; i++)
                {
                    modelErrorMessages.Contains(errorMessages[i]).ShouldBeTrue();
                }
            }
        }
    }
}