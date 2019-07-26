using System;
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Label;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Tests.Common.Requests.Label
{
    public class LabelCreateListRequestTests
    {

        [Test]
        public void LabelCreateListRequest_Constructor()
        {
            var labels = GetLabelListInfoList();
            var request = GetLabelCreateListRequest(CurrentUserId, UidOne, UidTwo, labels);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.ProjectUid.ShouldBe(UidTwo);
            request.Labels.ShouldBe(labels);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidTwo, GetLabelListInfoList());
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyUid, GetLabelListInfoList());
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelCreateListRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid projectUid,
            List<LabelListInfo> labels)
        {
            Assert.Throws<ArgumentException>(() => { new LabelCreateListRequest(currentUserId, organizationUid, projectUid, labels); });
        }
    }
}