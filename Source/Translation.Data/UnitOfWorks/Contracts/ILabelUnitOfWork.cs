using System.Collections.Generic;
using System.Threading.Tasks;

using Translation.Data.Entities.Domain;

namespace Translation.Data.UnitOfWorks.Contracts
{
    public interface ILabelUnitOfWork
    {
        Task<bool> DoCreateWork(long currentUserId, Label label);
        Task<bool> DoCreateWorkBulk(long currentUserId, List<Label> labels, List<LabelTranslation> labelTranslations, List<LabelTranslation> oldTranslations);
        Task<bool> DoDeleteWork(long currentUserId, Label label);
        Task<bool> DoCloneWork(long currentUserId, long labelId, Label newLabel);

        Task<bool> DoCreateTranslationWork(long currentUserId, LabelTranslation labelTranslation);
        Task<bool> DoCreateTranslationWorkBulk(long currentUserId, List<LabelTranslation> labelTranslations);
        Task<bool> DoDeleteTranslationWork(long currentUserId, LabelTranslation labelTranslation);
    }
}