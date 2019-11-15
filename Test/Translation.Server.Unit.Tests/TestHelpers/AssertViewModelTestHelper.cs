using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Shouldly;
using StandardUtils.Models.Shared;

using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;

namespace Translation.Server.Unit.Tests.TestHelpers
{
    public class AssertViewModelTestHelper
    {
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
            PagingInfo pagingInfo = new PagingInfo();
            SetPagingInfoForSelectAfter(pagingInfo);

            pagingInfo.Skip.ShouldBe(info.Skip);
            pagingInfo.Take.ShouldBe(info.Take);
            pagingInfo.LastUid.ShouldBe(info.LastUid);
            pagingInfo.IsAscending.ShouldBe(info.IsAscending);
            pagingInfo.TotalItemCount.ShouldBe(totalItemCountOfPagingInfo);
        }

        public static void AssertPagingInfoForSelectMany(PagingInfo info, int totalItemCountOfPagingInfo)
        {
            PagingInfo pagingInfo = new PagingInfo();
            SetPagingInfoForSelectMany(pagingInfo);

            pagingInfo.Skip.ShouldBe(info.Skip);
            pagingInfo.Take.ShouldBe(info.Take);
            pagingInfo.LastUid.ShouldBe(info.LastUid);
            pagingInfo.IsAscending.ShouldBe(info.IsAscending);
            pagingInfo.TotalItemCount.ShouldBe(totalItemCountOfPagingInfo);
        }
    }
}