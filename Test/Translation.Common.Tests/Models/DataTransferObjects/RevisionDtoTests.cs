using System;

using NUnit.Framework;
using Shouldly;

using StandardUtils.Models.DataTransferObjects;

using static Translation.Common.Tests.TestHelpers.AssertPropertyTestHelper;

namespace Translation.Common.Tests.Models.DataTransferObjects
{
    [TestFixture]
    public class RevisionDtoTests
    {
        [Test]
        public void RevisionDto()
        {
            var dto = new RevisionDto<BaseDto>();

            var dtoType = dto.GetType();
            var properties = dtoType.GetProperties();

            dtoType.BaseType.Name.ShouldBe(nameof(Object));

            AssertIntegerProperty(properties, "Revision", dto.Revision);
            AssertGuidProperty(properties, "RevisionedByUid", dto.RevisionedByUid);
            AssertStringProperty(properties, "RevisionedByName", dto.RevisionedByName);
            AssertDateTimeProperty(properties, "RevisionedAt", dto.RevisionedAt);
            AssertObjectProperty(properties, "Item", dto.Item);
        }
    }
}