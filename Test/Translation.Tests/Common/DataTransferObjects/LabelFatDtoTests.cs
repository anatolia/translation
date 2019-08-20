using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using Shouldly;

using Translation.Common.Models.Base;
using Translation.Common.Models.DataTransferObjects;
using static Translation.Tests.TestHelpers.AssertPropertyTestHelper;
namespace Translation.Tests.Common.DataTransferObjects
{
    [TestFixture]
    public class LabelFatDtoTests
    {
        [Test]
        public void LabelFatDto()
        {
            var dto = new LabelFatDto();

            var dtoType = dto.GetType();
            var properties = dtoType.GetProperties();

            AssertGuidProperty(properties, "Uid", dto.Uid);
            AssertStringProperty(properties, "Key", dto.Key);
            var propertyValue = dto.Translations;
            propertyValue.ShouldBeAssignableTo<List<LabelTranslationSlimDto>>();
            var propertyInfo = properties.First(x => x.Name == "Translations");
            propertyInfo.Name.ShouldNotBeNull();
        }
    }
}