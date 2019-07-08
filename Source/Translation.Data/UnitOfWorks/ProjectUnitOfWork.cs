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
            await _transactionalExecutor.ExecuteAsync<bool>(async connection =>
            {
                _organizationRepository.SetSqlExecutorForTransaction(connection);
                _projectRepository.SetSqlExecutorForTransaction(connection);

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
            await _transactionalExecutor.ExecuteAsync<bool>(async connection =>
            {
                _organizationRepository.SetSqlExecutorForTransaction(connection);
                _projectRepository.SetSqlExecutorForTransaction(connection);

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
            await _transactionalExecutor.ExecuteAsync<bool>(async connection =>
            {
                _organizationRepository.SetSqlExecutorForTransaction(connection);
                _projectRepository.SetSqlExecutorForTransaction(connection);
                _labelRepository.SetSqlExecutorForTransaction(connection);
                _labelTranslationRepository.SetSqlExecutorForTransaction(connection);

                var labels = await _labelRepository.SelectAll(x => x.ProjectId == projectId);
                var labelTranslations = await _labelTranslationRepository.SelectAll(x => x.ProjectId == projectId);

                newProject.LabelTranslationCount = labelTranslations.Count;
                newProject.LabelCount = labels.Count;
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

                    var labelId = await _labelRepository.Insert(currentUserId, label);

                    var labelsTranslations = labelTranslations.Where(x => x.LabelName == label.Key).ToList();
                    for (var lt = 0; lt < labelsTranslations.Count; lt++)
                    {
                        var labelTranslation = labelTranslations[lt];
                        labelTranslation.Uid = Guid.NewGuid();
                        labelTranslation.ProjectId = newProjectId;
                        labelTranslation.ProjectUid = newProject.Uid;
                        labelTranslation.ProjectName = newProject.Name;
                        labelTranslation.LabelId = labelId;

                        await _labelTranslationRepository.Insert(currentUserId, labelTranslation);
                    }
                }

                return true;
            });

            return true;
        }
    }
}