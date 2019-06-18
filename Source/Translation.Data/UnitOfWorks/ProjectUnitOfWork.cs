using System;
using System.Linq;
using System.Threading.Tasks;

using StandardRepository.PostgreSQL;

using Translation.Data.Entities.Domain;
using Translation.Data.Repositories.Contracts;
using Translation.Data.UnitOfWorks.Contracts;

namespace Translation.Data.UnitOfWorks
{
    public class ProjectUnitOfWork : IProjectUnitOfWork
    {
        private readonly PostgreSQLTransactionalExecutor _transactionalExecutor;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ILabelRepository _labelRepository;
        private readonly ILabelTranslationRepository _labelTranslationRepository;

        public ProjectUnitOfWork(PostgreSQLTransactionalExecutor transactionalExecutor,
                                      IOrganizationRepository organizationRepository,
                                      IProjectRepository projectRepository,
                                      ILabelRepository labelRepository,
                                      ILabelTranslationRepository labelTranslationRepository)
        {
            _transactionalExecutor = transactionalExecutor;
            _organizationRepository = organizationRepository;
            _projectRepository = projectRepository;
            _labelRepository = labelRepository;
            _labelTranslationRepository = labelTranslationRepository;
        }

        public async Task<bool> DoCreateWork(long currentUserId, Project project)
        {
            await _transactionalExecutor.ExecuteAsync<bool>(async connectionFactory =>
            {
                _organizationRepository.SetSqlExecutorForTransaction(connectionFactory);
                _projectRepository.SetSqlExecutorForTransaction(connectionFactory);

                await _projectRepository.Insert(currentUserId, project);

                var organization = await _organizationRepository.SelectById(project.OrganizationId);
                organization.ProjectCount++;
                await _organizationRepository.Update(currentUserId, organization);

                return true;
            });

            return true;
        }

        public async Task<bool> DoDeleteWork(long currentUserId, Project project)
        {
            await _transactionalExecutor.ExecuteAsync<bool>(async connectionFactory =>
            {
                _organizationRepository.SetSqlExecutorForTransaction(connectionFactory);
                _projectRepository.SetSqlExecutorForTransaction(connectionFactory);

                await _projectRepository.Delete(currentUserId, project.Id);

                var organization = await _organizationRepository.SelectById(project.OrganizationId);
                organization.ProjectCount--;
                await _organizationRepository.Update(currentUserId, organization);

                return true;
            });

            return true;
        }

        public async Task<bool> DoCloneWork(long currentUserId, long projectId, Project newProject)
        {
            await _transactionalExecutor.ExecuteAsync<bool>(async connectionFactory =>
            {
                _organizationRepository.SetSqlExecutorForTransaction(connectionFactory);
                _projectRepository.SetSqlExecutorForTransaction(connectionFactory);
                _labelRepository.SetSqlExecutorForTransaction(connectionFactory);
                _labelTranslationRepository.SetSqlExecutorForTransaction(connectionFactory);

                var labels = await _labelRepository.SelectAll(x => x.ProjectId == projectId);
                var labelTranslations = await _labelTranslationRepository.SelectAll(x => x.ProjectId == projectId);

                newProject.LabelTranslationCount = labelTranslations.Count;
                newProject.LabelCount = labelTranslations.Count;
                var newProjectId = await _projectRepository.Insert(currentUserId, newProject);

                var organization = await _organizationRepository.SelectById(newProject.OrganizationId);
                organization.ProjectCount++;
                await _organizationRepository.Update(currentUserId, organization);

                for (var i = 0; i < labels.Count; i++)
                {
                    var label = labels[i];
                    label.Uid = Guid.NewGuid();
                    label.ProjectId = newProjectId;
                    label.ProjectUid = newProject.Uid;
                    label.ProjectName = newProject.Name;
                    label.LabelTranslationCount = labelTranslations.Count(x => x.LabelId == label.Id);

                    await _labelRepository.Insert(currentUserId, label);
                }

                for (var i = 0; i < labelTranslations.Count; i++)
                {
                    var labelTranslation = labelTranslations[i];
                    labelTranslation.Uid = Guid.NewGuid();
                    labelTranslation.ProjectId = newProjectId;
                    labelTranslation.ProjectUid = newProject.Uid;
                    labelTranslation.ProjectName = newProject.Name;

                    await _labelTranslationRepository.Insert(currentUserId, labelTranslation);
                }

                return true;
            });

            return true;
        }
    }
}