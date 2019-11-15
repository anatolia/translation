using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Autofac;
using StandardRepository.Helpers;
using StandardRepository.Models;
using StandardRepository.PostgreSQL.DbGenerator;
using StandardRepository.PostgreSQL.Factories;
using StandardRepository.PostgreSQL.Helpers;
using StandardRepository.PostgreSQL.Helpers.SqlExecutor;
using StandardUtils.Models.Shared;

using Translation.Common.Contracts;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Parameter;
using Translation.Data.Factories;
using Translation.Data.Repositories.Contracts;

namespace Translation.Client.Web.Helpers
{
    public class DbGeneratorHelper
    {
        public void Generate(ILifetimeScope container, string webRootPath)
        {
            var connectionSettings = container.Resolve<ConnectionSettings>();
            var typeLookup = container.Resolve<PostgreSQLTypeLookup>();
            var entityUtils = container.Resolve<EntityUtils>();

            var masterConnectionString = PostgreSQLConnectionFactory.GetConnectionString(connectionSettings.DbHost, connectionSettings.DbNameMaster, connectionSettings.DbUser, connectionSettings.DbPassword, connectionSettings.DbPort);
            var masterConnectionFactory = new PostgreSQLConnectionFactory(masterConnectionString);
            var sqlExecutorMaster = new PostgreSQLExecutor(masterConnectionFactory, entityUtils);

            var connectionString = PostgreSQLConnectionFactory.GetConnectionString(connectionSettings);
            var connectionFactory = new PostgreSQLConnectionFactory(connectionString);
            var sqlExecutor = new PostgreSQLExecutor(connectionFactory, entityUtils);

            var organizationService = container.Resolve<IOrganizationService>();

            var dbGenerator = new PostgreSQLDbGenerator(typeLookup, entityUtils, sqlExecutorMaster, sqlExecutor);
            if (!dbGenerator.IsDbExistsDb(connectionSettings.DbName))
            {
                dbGenerator.CreateDb(connectionSettings.DbName);
                dbGenerator.Generate().Wait();

                var translationProviderRepository = container.Resolve<ITranslationProviderRepository>();
                var translationProviderFactory = container.Resolve<TranslationProviderFactory>();
                var (yandex, google) = InsertTranslationProviders(translationProviderRepository, translationProviderFactory);
                
                var languageRepository = container.Resolve<ILanguageRepository>();
                var languageFactory = container.Resolve<LanguageFactory>();
                var (turkish, english) = InsertLanguages(languageRepository, languageFactory);

                var organizationRepository = container.Resolve<IOrganizationRepository>();
                var userRepository = container.Resolve<IUserRepository>();
                var projectRepository = container.Resolve<IProjectRepository>();
                var adminSettings = container.Resolve<AdminSettings>();
                var organizationId = InsertAdmin(adminSettings, organizationService, organizationRepository, userRepository, projectRepository, languageRepository);

                var project = projectRepository.Select(x => x.OrganizationId == organizationId).Result;
                project.LanguageId = english.Id;
                project.LanguageUid = english.Uid;
                project.LanguageName = english.Name;
                project.LanguageIconUrl = english.IconUrl;

                var superAdmin = userRepository.Select(x => x.Email == adminSettings.AdminEmail).Result;
                superAdmin.IsSuperAdmin = true;

                projectRepository.Update(superAdmin.Id, project);

                var labelService = container.Resolve<ILabelService>();
                InsertLabels(labelService, project, webRootPath);
            }

            organizationService.LoadOrganizationsToCache();
            organizationService.LoadUsersToCache();
        }

        private long InsertAdmin(AdminSettings adminSettings, IOrganizationService organizationService, IOrganizationRepository organizationRepository, 
                                 IUserRepository userRepository, IProjectRepository projectRepository, ILanguageRepository languageRepository)
        {
            organizationService.CreateOrganizationWithAdmin(new SignUpRequest(ConstantHelper.ORGANIZATION_NAME, adminSettings.AdminFirstName, adminSettings.AdminLastName,
                                                            adminSettings.AdminEmail, adminSettings.AdminPassword, new ClientLogInfo())).Wait();

            var superAdmin = userRepository.Select(x => x.Email == adminSettings.AdminEmail).Result;
            superAdmin.IsSuperAdmin = true;

            var english = languageRepository.Select(x => x.IsoCode2Char == "en").Result;
            superAdmin.LanguageId = english.Id;
            superAdmin.LanguageUid = english.Uid;
            superAdmin.LanguageName = english.Name;
            superAdmin.LanguageIconUrl = english.IconUrl;
            userRepository.Update(superAdmin.Id, superAdmin);

            var organization = organizationRepository.Select(x => x.Name == ConstantHelper.ORGANIZATION_NAME).Result;
            organization.IsSuperOrganization = true;
            organization.ProjectCount = 1;
            organizationRepository.Update(superAdmin.Id, organization);

            var superProject = projectRepository.Select(x => x.OrganizationId == organization.Id).Result;
            superProject.IsSuperProject = true;
            projectRepository.Update(superAdmin.Id, superProject).Wait();

            return organization.Id;
        }

        private (Language, Language) InsertLanguages(ILanguageRepository languageRepository, LanguageFactory languageFactory)
        {
            var english = languageFactory.CreateEntity("en", "eng", "English", "English");
            var chinese = languageFactory.CreateEntity("zh", "zho", "Simplified Chinese", "简化字");
            var spanish = languageFactory.CreateEntity("es", "spa", "Spanish", "Español");
            var hindi = languageFactory.CreateEntity("hi", "hin", "Hindi", "हिन्दी");
            var arabic = languageFactory.CreateEntity("ar", "ara", "Arabic", "العربية");
            var portuguese = languageFactory.CreateEntity("pt", "por", "Portuguese", "Português");
            var russian = languageFactory.CreateEntity("ru", "rus", "Russian", "русский");
            var japanese = languageFactory.CreateEntity("ja", "jpn", "Japanese", "日本語");
            var turkish = languageFactory.CreateEntity("tr", "tur", "Turkish", "Türkçe");

            languageRepository.Insert(0, english).Wait();
            
            languageRepository.Insert(0, chinese).Wait();
            languageRepository.Insert(0, spanish).Wait();
            languageRepository.Insert(0, hindi).Wait();
            languageRepository.Insert(0, arabic).Wait();
            languageRepository.Insert(0, portuguese).Wait();
            languageRepository.Insert(0, russian).Wait();
            languageRepository.Insert(0, japanese).Wait();
            languageRepository.Insert(0, turkish).Wait();

            return (turkish, english);
        }

        private (TranslationProvider, TranslationProvider) InsertTranslationProviders(ITranslationProviderRepository translationProviderRepository, TranslationProviderFactory translationProviderFactory)
        {
            var google = translationProviderFactory.CreateEntity("google");
            var yandex = translationProviderFactory.CreateEntity("yandex");

            translationProviderRepository.Insert(0, google).Wait();
            translationProviderRepository.Insert(0, yandex).Wait();

            return (yandex, google);
        }

        private void InsertLabels(ILabelService labelService, Project project, string webRootPath)
        {
            var labelsFilePath = Path.Combine(webRootPath, "files", "projectTranslations.csv");
            if (!File.Exists(labelsFilePath))
            {
                return;
            }

            var labelListInfos = new List<LabelListInfo>();
            var lines = File.ReadAllLines(labelsFilePath, Encoding.UTF8);
            if (!lines.Any())
            {
                return;
            }

            for (var i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');
                if (values.Length != 3)
                {
                    throw new Exception("projectTranslations.csv is not valid!");
                }

                labelListInfos.Add(new LabelListInfo
                {
                    LabelKey = values[0],
                    LanguageIsoCode2 = values[1],
                    Translation = values[2]
                });
            }
            
            var request = new LabelCreateListRequest(project.CreatedBy, project.OrganizationUid, project.Uid, false, labelListInfos);
            var response = labelService.CreateLabelFromList(request).Result;
            if (response.Status.IsNotSuccess)
            {
                throw new Exception("couldn't add project translations!");
            }
        }
    }
}