using NUnit.Framework;

using Cheviri.Common.Helpers;

namespace Cheviri.Tests.HelperTests
{
    [TestFixture]
    public class StringHelperTests
    {
        [Test]
        public void StringHelpers_GetNewUid()
        {
            // arrange
            // act
            var result = StringHelper.GetNewUid();

            // assert
            Assert.IsAssignableFrom<string>(result);
            Assert.True(result.IsUid());
            Assert.False(result.IsNotUid());
        }
    }
}