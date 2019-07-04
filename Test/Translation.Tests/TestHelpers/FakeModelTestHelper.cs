using Translation.Common.Models.Shared;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.TestHelpers
{
    public class FakeModelTestHelper
    {
        public static PagingInfo GetFakePagingInfo()
        {
            var pagingInfo = new PagingInfo();
            pagingInfo.Skip = One;
            pagingInfo.Take = Two;
            pagingInfo.LastUid = UidOne;
            pagingInfo.IsAscending = BooleanTrue;
            pagingInfo.TotalItemCount = Ten;
            pagingInfo.Type = PagingInfo.PAGE_NUMBERS;

            return pagingInfo;
        }


        public static PagingInfo GetFakePagingInfoForSelectMany()
        {
            var pagingInfo = new PagingInfo();
            pagingInfo.Skip = One;
            pagingInfo.Take = Two;
            pagingInfo.LastUid = UidOne;
            pagingInfo.IsAscending = BooleanTrue;
            pagingInfo.TotalItemCount = Ten;
            pagingInfo.Type = PagingInfo.PAGE_NUMBERS;

            return pagingInfo;
        }

        public static PagingInfo GetFakePagingInfoForSelectAfter()
        {
            var pagingInfo = new PagingInfo();
            pagingInfo.Skip = Zero;
            pagingInfo.Take = Two;
            pagingInfo.LastUid = UidOne;
            pagingInfo.IsAscending = BooleanTrue;
            pagingInfo.TotalItemCount = Ten;
            pagingInfo.Type = PagingInfo.PAGE_NUMBERS;

            return pagingInfo;
        }
    }
}