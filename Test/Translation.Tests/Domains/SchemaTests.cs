using System;

using NUnit.Framework;
using StandardRepository.Models.Entities.Schemas;

using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using Translation.Data.Entities.Parameter;

namespace Translation.Tests.DomainTests
{
    [TestFixture]
    public class SchemaTests
    {
        [TestCase(typeof(Project)), 
         TestCase(typeof(Label)), 
         TestCase(typeof(LabelTranslation))]
        public void ProjectEntityClasses_Has_ISchemaProject(Type entity)
        {
            var instance = (ISchemaDomain)Activator.CreateInstance(entity);
            Assert.IsNotNull(instance);
        }

        [TestCase(typeof(Language)),
         TestCase(typeof(Word)),
         TestCase(typeof(WordTranslation))]
        public void ParameterEntityClasses_Has_ISchemaParameter(Type entity)
        {
            var instance = (ISchemaParameter)Activator.CreateInstance(entity);
            Assert.IsNotNull(instance);
        }

        [TestCase(typeof(Organization)), 
         TestCase(typeof(Integration)),
         TestCase(typeof(IntegrationClient)),
         TestCase(typeof(Token)),
         TestCase(typeof(TokenRequestLog)),
         TestCase(typeof(User)),
         TestCase(typeof(UserLoginLog)),
         TestCase(typeof(SendEmailLog)),
         TestCase(typeof(Journal))]
        public void MainEntityClasses_Has_ISchemaMain(Type entity)
        {
            var instance = (ISchemaMain)Activator.CreateInstance(entity);
            Assert.IsNotNull(instance);
        }
    }
}