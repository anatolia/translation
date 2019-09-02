using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using StandardRepository.Helpers;
using StandardRepository.Models;
using StandardRepository.PostgreSQL;

using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Main;
using Translation.Data.Repositories.Contracts;
using Translation.Data.UnitOfWorks.Contracts;

namespace Translation.Data.UnitOfWorks
{
    public class SignUpUnitOfWork : ISignUpUnitOfWork
    {
        private readonly PostgreSQLTransactionalExecutor _transactionalExecutor;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserLoginLogRepository _userLoginLogRepository;
        private readonly IIntegrationRepository _integrationRepository;
        private readonly IIntegrationClientRepository _integrationClientRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ILabelRepository _labelRepository;
        private readonly ILabelTranslationRepository _labelTranslationRepository;

        public SignUpUnitOfWork(PostgreSQLTransactionalExecutor transactionalExecutor,
                                IOrganizationRepository organizationRepository,
                                IUserRepository userRepository,
                                IUserLoginLogRepository userLoginLogRepository,
                                IIntegrationRepository integrationRepository,
                                IIntegrationClientRepository integrationClientRepository,
                                IProjectRepository projectRepository,
                                ILabelRepository labelRepository,
                                ILabelTranslationRepository labelTranslationRepository)
        {
            _transactionalExecutor = transactionalExecutor;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _userLoginLogRepository = userLoginLogRepository;
            _integrationRepository = integrationRepository;
            _integrationClientRepository = integrationClientRepository;
            _projectRepository = projectRepository;
            _labelRepository = labelRepository;
            _labelTranslationRepository = labelTranslationRepository;
        }

        public async Task<(bool, Organization, User)> DoWork(Organization organization, User user, UserLoginLog userLoginLog,
                                                             Integration integration, IntegrationClient integrationClient, Project project)
        {
            var (organizationResult,
                 userResult) = await _transactionalExecutor.ExecuteAsync(async connection =>
            {
                _userRepository.SetSqlExecutorForTransaction(connection);
                _organizationRepository.SetSqlExecutorForTransaction(connection);
                _userLoginLogRepository.SetSqlExecutorForTransaction(connection);
                _integrationRepository.SetSqlExecutorForTransaction(connection);
                _integrationClientRepository.SetSqlExecutorForTransaction(connection);
                _projectRepository.SetSqlExecutorForTransaction(connection);

                var organizationId = await _organizationRepository.Insert(0, organization);
                organization.Id = organizationId;

                user.OrganizationId = organizationId;
                var userId = await _userRepository.Insert(0, user);
                user.Id = userId;

                userLoginLog.OrganizationId = organizationId;
                userLoginLog.UserId = userId;
                await _userLoginLogRepository.Insert(userId, userLoginLog);

                organization.CreatedBy = userId;
                organization.UserCount++;
                user.CreatedBy = userId;
                await _organizationRepository.Update(userId, organization);
                await _userRepository.Update(userId, user);

                integration.OrganizationId = organizationId;
                var integrationId = await _integrationRepository.Insert(userId, integration);

                integrationClient.OrganizationId = organizationId;
                integrationClient.IntegrationId = integrationId;
                await _integrationClientRepository.Insert(userId, integrationClient);

                project.OrganizationId = organizationId;
                var projectId = await _projectRepository.Insert(userId, project);
                project.Id = projectId;
                if (!organization.IsSuperOrganization)
                {
                    var superProject = await _projectRepository.Select(x => x.IsSuperProject);
                    if (superProject.IsExist())
                    {
                        var labels = await _labelRepository.SelectAll(x => x.ProjectId == projectId, false,
                                                                      new List<OrderByInfo<Label>>() { new OrderByInfo<Label>(x => x.Id) });

                        var labelTranslations = await _labelTranslationRepository.SelectAll(x => x.ProjectId == projectId, false,
                                                                                            new List<OrderByInfo<LabelTranslation>>() { new OrderByInfo<LabelTranslation>(x => x.Id) });

                        project.LabelCount = labels.Count;
                        organization.ProjectCount++;
                        organization.LabelCount = labels.Count;
                        await _projectRepository.Update(userId, project);
                        for (var i = 0; i < labels.Count; i++)
                        {
                            var label = labels[i];
                            label.Uid = Guid.NewGuid();
                            label.OrganizationId = project.OrganizationId;
                            label.OrganizationUid = project.OrganizationUid;
                            label.OrganizationName = project.OrganizationName;
                            label.ProjectId = projectId;
                            label.ProjectUid = project.Uid;
                            label.ProjectName = project.Name;

                            var labelId = await _labelRepository.Insert(userId, label);

                            var labelsTranslations = labelTranslations.Where(x => x.LabelName == label.Name).ToList();
                            for (var j = 0; j < labelsTranslations.Count; j++)
                            {
                                var labelTranslation = labelsTranslations[j];
                                labelTranslation.Uid = Guid.NewGuid();
                                labelTranslation.OrganizationId = project.OrganizationId;
                                labelTranslation.OrganizationUid = project.OrganizationUid;
                                labelTranslation.OrganizationName = project.OrganizationName;
                                labelTranslation.ProjectId = projectId;
                                labelTranslation.ProjectUid = project.Uid;
                                labelTranslation.ProjectName = project.Name;
                                labelTranslation.LabelId = labelId;
                                labelTranslation.LabelUid = label.Uid;
                                labelTranslation.LabelName = label.Name;

                                await _labelTranslationRepository.Insert(userId, labelTranslation);

                                project.LabelTranslationCount++;
                                organization.LabelTranslationCount++;
                            }
                        }
                        await _projectRepository.Update(userId, project);
                        await _organizationRepository.Update(userId, organization);
                    }
                }

                return (organization,
                        user);
            });

            return (true,
                    organizationResult,
                    userResult);
        }
    }
}