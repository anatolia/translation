using NUnit.Framework;

using Translation.Common.Helpers;

namespace Translation.Tests.HelperTests
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