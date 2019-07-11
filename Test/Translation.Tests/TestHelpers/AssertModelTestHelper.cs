using Microsoft.AspNetCore.Mvc;
using Shouldly;

using Translation.Client.Web.Models.Base;
using Translation.Common.Models.Shared;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.TestHelpers
{
    public class AssertModelTestHelper
    {

        public static void AssertPagingInfo(PagingInfo info)
        {
            var pagingInfo = GetFakePagingInfo();

            info.ShouldSatisfyAllConditions(() => info.Skip.ShouldBe(pagingInfo.Skip),
                                            () => info.Take.ShouldBe(pagingInfo.Take),
                                            () => info.LastUid.ShouldBe(pagingInfo.LastUid),
                                            () => info.IsAscending.ShouldBe(pagingInfo.IsAscending),
                                            () => info.TotalItemCount.ShouldBe(pagingInfo.TotalItemCount));
        }

        public static void AssertPagingInfo(JsonResult result)
        {
            var pagingInfo = new PagingInfo();

            var resultValue = (DataResult)result.Value;
            resultValue.PagingInfo.Type.ShouldBe(PagingInfo.PAGE_NUMBERS);
            resultValue.PagingInfo.Take.ShouldBe(pagingInfo.Take);
            resultValue.PagingInfo.Skip.ShouldBe(pagingInfo.Skip);
        }

        public static void AssertPagingInfoForSelectMany(PagingInfo info)
        {
            var pagingInfo = GetFakePagingInfoForSelectMany();

            info.ShouldSatisfyAllConditions(() => info.Skip.ShouldBe(pagingInfo.Skip),
                                            () => info.Take.ShouldBe(pagingInfo.Take),
                                            () => info.LastUid.ShouldBe(pagingInfo.LastUid),
                                            () => info.IsAscending.ShouldBe(pagingInfo.IsAscending),
                                            () => info.TotalItemCount.ShouldBe(pagingInfo.TotalItemCount));
        }

        public static void AssertPagingInfoForSelectAfter(PagingInfo info)
        {
            var pagingInfo = GetFakePagingInfoForSelectAfter();

            info.ShouldSatisfyAllConditions(() => info.Skip.ShouldBe(pagingInfo.Skip),
                                            () => info.Take.ShouldBe(pagingInfo.Take),
                                            () => info.LastUid.ShouldBe(pagingInfo.LastUid),
                                            () => info.IsAscending.ShouldBe(pagingInfo.IsAscending),
                                            () => info.TotalItemCount.ShouldBe(pagingInfo.TotalItemCount));
        }
    }
}