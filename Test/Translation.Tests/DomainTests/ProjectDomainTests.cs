using System.Collections.Generic;

using NUnit.Framework;

using Translation.Data.Entities.Main;
using Translation.Data.Entities.Project;
using Translation.Data.Entities.Parameter;

namespace Translation.Tests.DomainTests
{
    [TestFixture]
    public class ProjectDomainTests
    {
        [Test]
        public void Organization_Has_Projects()
        {
            var organization = new Organization();
            Assert.IsAssignableFrom<List<Project>>(organization.Projects);
        }

        [Test]
        public void Project_Keeps_LabelCount()
        {
            var project = new Project();
            Assert.IsAssignableFrom<int>(project.LabelCount);
        }

        [Test]
        public void Label_Has_Translations()
        {
            var label = new Label();
            Assert.IsAssignableFrom<List<LabelTranslation>>(label.Translations);
        }

        [Test]
        public void Translation_Has_Language()
        {
            var languageProperty = typeof(LabelTranslation).GetProperty(nameof(Language));
            Assert.True(languageProperty.PropertyType.Name == typeof(Language).Name);
        }

        [Test]
        public void Organization_Has_Integrations()
        {
            var organization = new Organization();
            Assert.IsAssignableFrom<List<Integration>>(organization.Integrations);
        }

        [Test]
        public void Integration_Has_Roles()
        {
            var integration = new Integration();
            Assert.IsAssignableFrom<List<Role>>(integration.Roles);
        }

        [Test]
        public void Organization_Has_Users()
        {
            var organization = new Organization();
            Assert.IsAssignableFrom<List<User>>(organization.Users);
        }

        [Test]
        public void User_Has_Roles()
        {
            var user = new User();
            Assert.IsAssignableFrom<List<Role>>(user.Roles);
        }

        [Test]
        public void Role_Has_Permissions()
        {
            var role = new Role();
            Assert.IsAssignableFrom<List<Permission>>(role.Permissions);
        }
    }
}