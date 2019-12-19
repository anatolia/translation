using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Journal;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Journal
{
    [TestFixture]
    public class OrganizationJournalReadListRequestTests
    {

        [Test]
        public void ProjectReadRequest_Constructor()
        {
            var request = GetOrganizationJournalReadListRequest(CurrentUserId);

            request.CurrentUserId.ShouldBe(CurrentUserId);
        }
    }
}