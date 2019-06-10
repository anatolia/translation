using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using StandardRepository.PostgreSQL;

using Translation.Data.Entities.Domain;
using Translation.Data.Repositories.Contracts;
using Translation.Data.UnitOfWorks.Contracts;

namespace Translation.Data.UnitOfWorks
{
    public class LabelUnitOfWork : ILabelUnitOfWork
    {
        private readonly PostgreSQLTransactionalExecutor _transactionalExecutor;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ILabelRepository _labelRepository;
        private readonly ILabelTranslationRepository _labelTranslationRepository;

        public LabelUnitOfWork(PostgreSQLTransactionalExecutor transactionalExecutor,
                               IOrganizationRepository organizationRepository,
                               IUserRepository userRepository,
                               IProjectRepository projectRepository,
                               ILabelRepository labelRepository,
                               ILabelTranslationRepository labelTranslationRepository)
        {
            _transactionalExecutor = transactionalExecutor;
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _labelRepository = labelRepository;
            _labelTranslationRepository = labelTranslationRepository;
        }

        public async Task<bool> DoCreateWork(long currentUserId, Label label)
        {
            await _transactionalExecutor.ExecuteAsync<bool>(async connectionFactory =>
            {
                _organizationRepository.SetSqlExecutorForTransaction(connectionFactory);
                _userRepository.SetSqlExecutorForTransaction(connectionFactory);
                _projectRepository.SetSqlExecutorForTransaction(connectionFactory);
                _labelRepository.SetSqlExecutorForTransaction(connectionFactory);

                await _labelRepository.Insert(currentUserId, label);

                var organization = await _organizationRepository.SelectById(label.OrganizationId);
                organization.LabelCount++;
                await _organizationRepository.Update(currentUserId, organization);

                var project = await _projectRepository.SelectById(label.ProjectId);
                project.LabelCount++;
                await _projectRepository.Update(currentUserId, project);

                var user = await _userRepository.SelectById(currentUserId);
                user.LabelCount++;
                await _userRepository.Update(currentUserId, user);

                return true;
            });

            return true;
        }

        public async Task<bool> DoCreateWorkBulk(long currentUserId, List<Label> labels, List<LabelTranslation> labelTranslations)
        {
            await _transactionalExecutor.ExecuteAsync<bool>(async connectionFactory =>
            {
                _organizationRepository.SetSqlExecutorForTransaction(connectionFactory);
                _userRepository.SetSqlExecutorForTransaction(connectionFactory);
                _projectRepository.SetSqlExecutorForTransaction(connectionFactory);
                _labelRepository.SetSqlExecutorForTransaction(connectionFactory);
                _labelTranslationRepository.SetSqlExecutorForTransaction(connectionFactory);

                var first = labelTranslations.First();
                var organizationId = first.OrganizationId;
                var projectId = first.ProjectId;

                for (var i = 0; i < labels.Count; i++)
                {
                    var label = labels[i];
                    var labelsTranslations = labelTranslations.Where(x => x.LabelName == label.Key).ToList();

                    label.LabelTranslationCount = labelsTranslations.Count;
                    var labelId = await _labelRepository.Insert(currentUserId, label);
                    
                    for (var j = 0; j < labelsTranslations.Count; j++)
                    {
                        var labelTranslation = labelsTranslations[j];
                        labelTranslation.LabelId = labelId;
                        await _labelTranslationRepository.Insert(currentUserId, labelTranslation);
                    }
                }

                var organization = await _organizationRepository.SelectById(organizationId);
                organization.LabelTranslationCount = organization.LabelTranslationCount + labelTranslations.Count;
                organization.LabelCount = organization.LabelCount + labels.Count;
                await _organizationRepository.Update(currentUserId, organization);

                var project = await _projectRepository.SelectById(projectId);
                project.LabelTranslationCount = project.LabelTranslationCount + labelTranslations.Count;
                project.LabelCount = project.LabelCount + labels.Count;
                await _projectRepository.Update(currentUserId, project);
                
                var user = await _userRepository.SelectById(currentUserId);
                user.LabelTranslationCount = user.LabelTranslationCount + labelTranslations.Count;
                user.LabelCount = user.LabelCount + labels.Count;
                await _userRepository.Update(currentUserId, user);

                return true;
            });

            return true;
        }

        public async Task<bool> DoDeleteWork(long currentUserId, Label label)
        {
            await _transactionalExecutor.ExecuteAsync<bool>(async connectionFactory =>
            {
                _organizationRepository.SetSqlExecutorForTransaction(connectionFactory);
                _userRepository.SetSqlExecutorForTransaction(connectionFactory);
                _projectRepository.SetSqlExecutorForTransaction(connectionFactory);
                _labelRepository.SetSqlExecutorForTransaction(connectionFactory);

                await _labelRepository.Delete(currentUserId, label.Id);

                var organization = await _organizationRepository.SelectById(label.OrganizationId);
                organization.LabelCount--;
                await _organizationRepository.Update(currentUserId, organization);

                var project = await _projectRepository.SelectById(label.ProjectId);
                project.LabelCount--;
                await _projectRepository.Update(currentUserId, project);

                var user = await _userRepository.SelectById(currentUserId);
                user.LabelCount--;
                await _userRepository.Update(currentUserId, user);

                return true;
            });

            return true;
        }

        public async Task<bool> DoCloneWork(long currentUserId, long labelId, Label newLabel)
        {
            await _transactionalExecutor.ExecuteAsync(async connectionFactory =>
            {
                _organizationRepository.SetSqlExecutorForTransaction(connectionFactory);
                _userRepository.SetSqlExecutorForTransaction(connectionFactory);
                _projectRepository.SetSqlExecutorForTransaction(connectionFactory);
                _labelRepository.SetSqlExecutorForTransaction(connectionFactory);

                var newLabelId = await _labelRepository.Insert(currentUserId, newLabel);

                var organization = await _organizationRepository.SelectById(newLabel.OrganizationId);
                organization.LabelCount++;

                var project = await _projectRepository.SelectById(newLabel.ProjectId);
                project.LabelCount++;

                var user = await _userRepository.SelectById(currentUserId);
                user.LabelCount++;

                var labelTranslations = await _labelTranslationRepository.SelectAll(x => x.LabelId == labelId);
                foreach (var labelTranslation in labelTranslations)
                {
                    organization.LabelTranslationCount++;
                    project.LabelTranslationCount++;
                    user.LabelTranslationCount++;

                    labelTranslation.Uid = Guid.NewGuid();
                    labelTranslation.LabelId = newLabelId;
                    labelTranslation.LabelUid = newLabel.Uid;
                    labelTranslation.LabelName = newLabel.Name;
                    await _labelTranslationRepository.Insert(currentUserId, labelTranslation);
                }

                await _organizationRepository.Update(currentUserId, organization);
                await _projectRepository.Update(currentUserId, project);
                await _userRepository.Update(currentUserId, user);

                return true;
            });

            return true;
        }

        public async Task<bool> DoCreateTranslationWork(long currentUserId, LabelTranslation labelTranslation)
        {
            await _transactionalExecutor.ExecuteAsync<bool>(async connectionFactory =>
            {
                _organizationRepository.SetSqlExecutorForTransaction(connectionFactory);
                _userRepository.SetSqlExecutorForTransaction(connectionFactory);
                _projectRepository.SetSqlExecutorForTransaction(connectionFactory);
                _labelRepository.SetSqlExecutorForTransaction(connectionFactory);
                _labelTranslationRepository.SetSqlExecutorForTransaction(connectionFactory);

                await _labelTranslationRepository.Insert(currentUserId, labelTranslation);

                var organization = await _organizationRepository.SelectById(labelTranslation.OrganizationId);
                organization.LabelTranslationCount++;
                await _organizationRepository.Update(currentUserId, organization);

                var project = await _projectRepository.SelectById(labelTranslation.ProjectId);
                project.LabelTranslationCount++;
                await _projectRepository.Update(currentUserId, project);

                var label = await _labelRepository.SelectById(labelTranslation.LabelId);
                label.LabelTranslationCount++;
                await _labelRepository.Update(currentUserId, label);

                var user = await _userRepository.SelectById(currentUserId);
                user.LabelTranslationCount++;
                await _userRepository.Update(currentUserId, user);

                return true;
            });

            return true;
        }

        public async Task<bool> DoCreateTranslationWorkBulk(long currentUserId, List<LabelTranslation> labelTranslations)
        {
            await _transactionalExecutor.ExecuteAsync<bool>(async connectionFactory =>
            {
                _organizationRepository.SetSqlExecutorForTransaction(connectionFactory);
                _userRepository.SetSqlExecutorForTransaction(connectionFactory);
                _projectRepository.SetSqlExecutorForTransaction(connectionFactory);
                _labelRepository.SetSqlExecutorForTransaction(connectionFactory);
                _labelTranslationRepository.SetSqlExecutorForTransaction(connectionFactory);

                var first = labelTranslations.First();
                var organizationId = first.OrganizationId;
                var projectId = first.ProjectId;
                var labelId = first.LabelId;

                for (var i = 0; i < labelTranslations.Count; i++)
                {
                    var labelTranslation = labelTranslations[i];
                    await _labelTranslationRepository.Insert(currentUserId, labelTranslation);
                }

                var organization = await _organizationRepository.SelectById(organizationId);
                organization.LabelTranslationCount = organization.LabelTranslationCount + labelTranslations.Count;
                await _organizationRepository.Update(currentUserId, organization);

                var project = await _projectRepository.SelectById(projectId);
                project.LabelTranslationCount = project.LabelTranslationCount + labelTranslations.Count;
                await _projectRepository.Update(currentUserId, project);

                var label = await _labelRepository.SelectById(labelId);
                label.LabelTranslationCount = label.LabelTranslationCount + labelTranslations.Count;
                await _labelRepository.Update(currentUserId, label);

                var user = await _userRepository.SelectById(currentUserId);
                user.LabelTranslationCount = user.LabelTranslationCount + labelTranslations.Count;
                await _userRepository.Update(currentUserId, user);

                return true;
            });

            return true;
        }

        public async Task<bool> DoDeleteTranslationWork(long currentUserId, LabelTranslation labelTranslation)
        {
            await _transactionalExecutor.ExecuteAsync<bool>(async connectionFactory =>
            {
                _organizationRepository.SetSqlExecutorForTransaction(connectionFactory);
                _userRepository.SetSqlExecutorForTransaction(connectionFactory);
                _projectRepository.SetSqlExecutorForTransaction(connectionFactory);
                _labelRepository.SetSqlExecutorForTransaction(connectionFactory);
                _labelTranslationRepository.SetSqlExecutorForTransaction(connectionFactory);

                await _labelTranslationRepository.Delete(currentUserId, labelTranslation.Id);

                var organization = await _organizationRepository.SelectById(labelTranslation.OrganizationId);
                organization.LabelTranslationCount--;
                await _organizationRepository.Update(currentUserId, organization);

                var project = await _projectRepository.SelectById(labelTranslation.ProjectId);
                project.LabelTranslationCount--;
                await _projectRepository.Update(currentUserId, project);

                var label = await _labelRepository.SelectById(labelTranslation.LabelId);
                label.LabelTranslationCount--;
                await _labelRepository.Update(currentUserId, label);

                var user = await _userRepository.SelectById(currentUserId);
                user.LabelTranslationCount--;
                await _userRepository.Update(currentUserId, user);

                return true;
            });

            return true;
        }
    }
}