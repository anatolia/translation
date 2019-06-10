using Castle.Windsor;
using StandardRepository.Helpers;
using StandardRepository.Models;
using StandardRepository.PostgreSQL.DbGenerator;
using StandardRepository.PostgreSQL.Factories;
using StandardRepository.PostgreSQL.Helpers;
using StandardRepository.PostgreSQL.Helpers.SqlExecutor;

using Translation.Common.Contracts;
using Translation.Common.Models.Requests.Organization;
using Translation.Common.Models.Shared;
using Translation.Data.Factories;
using Translation.Data.Repositories.Contracts;

namespace Translation.Client.Web.Helpers
{
    public class DbGeneratorHelper
    {
        public static void Generate(WindsorContainer container)
        {
            var connectionSettings = container.Resolve<ConnectionSettings>();
            var adminSettings = container.Resolve<AdminSettings>();
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

                var organizationRepository = container.Resolve<IOrganizationRepository>();
                var userRepository = container.Resolve<IUserRepository>();
                InsertAdmin(adminSettings, organizationService, organizationRepository, userRepository);

                var languageRepository = container.Resolve<ILanguageRepository>();
                var languageFactory = container.Resolve<LanguageFactory>();
                InsertLanguages(languageRepository, languageFactory);
            }

            organizationService.LoadOrganizationsToCache();
            organizationService.LoadUsersToCache();
        }

        private static void InsertAdmin(AdminSettings adminSettings, IOrganizationService organizationService,
                                        IOrganizationRepository organizationRepository, IUserRepository userRepository)
        {
            organizationService.CreateOrganizationWithAdmin(new SignUpRequest(ConstantHelper.ORGANIZATION_NAME, adminSettings.AdminFirstName, adminSettings.AdminLastName,
                                                                              adminSettings.AdminEmail, adminSettings.AdminPassword, new ClientLogInfo())).Wait();

            var organization = organizationRepository.Select(x => x.Name == ConstantHelper.ORGANIZATION_NAME).Result;
            organization.IsSuperOrganization = true;
            organizationRepository.Update(0, organization);

            var superAdmin = userRepository.Select(x => x.Email == adminSettings.AdminEmail).Result;
            superAdmin.IsSuperAdmin = true;
            userRepository.Update(0, superAdmin);
        }

        private static void InsertLanguages(ILanguageRepository languageRepository, LanguageFactory languageFactory)
        {
            var chinese = languageFactory.CreateEntity("zh", "zho", "Simplified Chinese", "简化字");
            var spanish = languageFactory.CreateEntity("es", "spa", "Spanish", "Español");
            var english = languageFactory.CreateEntity("en", "eng", "English", "English");
            var hindi = languageFactory.CreateEntity("hi", "hin", "Hindi", "हिन्दी");
            var arabic = languageFactory.CreateEntity("ar", "ara", "Arabic", "العربية");
            var portuguese = languageFactory.CreateEntity("po", "por", "Portuguese", "Português");
            var russian = languageFactory.CreateEntity("ru", "rus", "Russian", "русский");
            var japanese = languageFactory.CreateEntity("ja", "jpn", "Japanese", "日本語");
            var turkish = languageFactory.CreateEntity("tr", "tur", "Turkish", "Türkçe");

            languageRepository.Insert(0, chinese).Wait();
            languageRepository.Insert(0, spanish).Wait();
            languageRepository.Insert(0, english).Wait();
            languageRepository.Insert(0, hindi).Wait();
            languageRepository.Insert(0, arabic).Wait();
            languageRepository.Insert(0, portuguese).Wait();
            languageRepository.Insert(0, russian).Wait();
            languageRepository.Insert(0, japanese).Wait();
            languageRepository.Insert(0, turkish).Wait();
        }
    }
}