using NUnit.Framework;

using Translation.Data.Entities.Domain;

namespace Translation.Tests.Domains
{
    [TestFixture]
    public class ProjectDomainTests
    {
        [Test]
        public void Project_Keeps_LabelCount()
        {
            var project = new Project();
            Assert.IsAssignableFrom<int>(project.LabelCount);
        }
    }
}