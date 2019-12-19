using NUnit.Framework;
using Shouldly;

using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Project
{
    [TestFixture]
    public class ProjectReadListRequestTests
    {
        [Test]
        public void ProjectReadListRequest_Constructor()
        {
            var request =GetProjectReadListRequest(CurrentUserId,UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
        }
    }
}