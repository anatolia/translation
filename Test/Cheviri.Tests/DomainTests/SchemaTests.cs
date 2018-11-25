using System;

using NUnit.Framework;

using Cheviri.Data.Entities.Base.Schemas;
using Cheviri.Data.Entities.Main;
using Cheviri.Data.Entities.Parameter;
using Cheviri.Data.Entities.Project;

namespace Cheviri.Tests.DomainTests
{
    [TestFixture]
    public class SchemaTests
    {
        [TestCase(typeof(Organization))]
        [TestCase(typeof(Project))]
        [TestCase(typeof(Label))]
        [TestCase(typeof(LabelTranslation))]
        public void ProjectEntityClasses_Has_ISchemaProject(Type entity)
        {
            var instance = (ISchemaProject) Activator.CreateInstance(entity);
            Assert.IsNotNull(instance);
        }

        [TestCase(typeof(Language))]
        [TestCase(typeof(Word))]
        [TestCase(typeof(WordTranslation))]
        public void ParameterEntityClasses_Has_ISchemaParameter(Type entity)
        {
            var instance = (ISchemaParameter)Activator.CreateInstance(entity);
            Assert.IsNotNull(instance);
        }

        [TestCase(typeof(Integration))]
        [TestCase(typeof(Token))]
        [TestCase(typeof(TokenRequestLog))]
        [TestCase(typeof(User))]
        [TestCase(typeof(UserLoginLog))]
        [TestCase(typeof(Role))]
        [TestCase(typeof(Permission))]
        [TestCase(typeof(PermissionLog))]
        [TestCase(typeof(Journal))]
        public void MainEntityClasses_Has_ISchemaMain(Type entity)
        {
            var instance = (ISchemaMain)Activator.CreateInstance(entity);
            Assert.IsNotNull(instance);
        }
    }
}