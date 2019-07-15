using Translation.Client.Web.Models.Project;
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

        public static ProjectCreateModel GetOrganizationOneProjectOneCreateModel()
        {
            var model = new ProjectCreateModel();
            model.OrganizationUid = OrganizationOneUid;
            model.Name = OrganizationOneProjectOneName;
            model.Url = HttpsUrl;

            return model;
        }

        public static ProjectEditModel GetOrganizationOneProjectOneEditModel()
        {
            var model = new ProjectEditModel();
            model.OrganizationUid = OrganizationOneUid;
            model.ProjectUid = OrganizationOneProjectOneUid;
            model.Name = OrganizationOneProjectOneName;
            model.Url = HttpsUrl;

            return model;
        }

        public static ProjectCloneModel GetOrganizationOneProjectOneCloneModel()
        {
            var model = new ProjectCloneModel();
            model.OrganizationUid = OrganizationOneUid;
            model.Name = OrganizationOneProjectOneName;
            model.CloningProjectUid = OrganizationOneProjectOneUid;
            model.LabelCount = One;
            model.LabelTranslationCount = Two;
            model.Url = HttpsUrl;

            return model;
        }
    }
}