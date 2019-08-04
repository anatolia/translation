using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Project;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Project
{
    [TestFixture]
    public class ProjectRestoreRequestTests
    {
        [Test]
        public void ProjectRestoreRequest_Constructor()
        {
            var request = GetProjectRestoreRequest(CurrentUserId, UidOne, One);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.ProjectUid.ShouldBe(UidOne);
            request.Revision.ShouldBe(One);
        }

    }

}
