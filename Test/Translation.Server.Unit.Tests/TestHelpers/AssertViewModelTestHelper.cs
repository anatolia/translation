using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Shouldly;

using Translation.Common.Models.Shared;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;

namespace Translation.Server.Unit.Tests.TestHelpers
{
    public class AssertViewModelTestHelper
    {
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

        public static void AssertView<T>(RedirectToActionResult result, string actionName)
        {
            result.ShouldNotBeNull();
            result.ActionName.ShouldBe(actionName);
        }

        public static void AssertReturnType<T>(T result)
        {
            result.ShouldNotBeNull();
            result.ShouldBeAssignableTo<T>();
        }

        public static void AssertPagingInfoForSelectAfter(PagingInfo info, int totalItemCountOfPagingInfo)
        {
            var pagingInfo = GetPagingInfoForSelectAfter();
            pagingInfo.Skip.ShouldBe(info.Skip);
            pagingInfo.Take.ShouldBe(info.Take);
            pagingInfo.LastUid.ShouldBe(info.LastUid);
            pagingInfo.IsAscending.ShouldBe(info.IsAscending);
            pagingInfo.TotalItemCount.ShouldBe(totalItemCountOfPagingInfo);
        }

        public static void AssertPagingInfoForSelectMany(PagingInfo info, int totalItemCountOfPagingInfo)
        {
            var pagingInfo = GetPagingInfoForSelectMany();
            pagingInfo.Skip.ShouldBe(info.Skip);
            pagingInfo.Take.ShouldBe(info.Take);
            pagingInfo.LastUid.ShouldBe(info.LastUid);
            pagingInfo.IsAscending.ShouldBe(info.IsAscending);
            pagingInfo.TotalItemCount.ShouldBe(totalItemCountOfPagingInfo);
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