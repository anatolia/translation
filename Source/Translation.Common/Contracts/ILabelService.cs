using System.Threading.Tasks;

using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Requests.Label.LabelTranslation;
using Translation.Common.Models.Responses.Label;
using Translation.Common.Models.Responses.Label.LabelTranslation;

namespace Translation.Common.Contracts
{
    public interface ILabelService
    {
        Task<LabelCreateResponse> CreateLabel(LabelCreateRequest request);
        Task<LabelCreateResponse> CreateLabel(LabelCreateWithTokenRequest request);
        Task<LabelCreateListResponse> CreateLabelFromList(LabelCreateListRequest request);
        Task<LabelReadResponse> GetLabel(LabelReadRequest request);
        Task<LabelReadListResponse> GetLabels(LabelReadListRequest request);
        Task<LabelRevisionReadListResponse> GetLabelRevisions(LabelRevisionReadListRequest request);
        Task<AllLabelReadListResponse> GetLabelsWithTranslations(AllLabelReadListRequest labelReadListRequest);
        Task<LabelEditResponse> EditLabel(LabelEditRequest request);
        Task<LabelDeleteResponse> DeleteLabel(LabelDeleteRequest request);
        Task<LabelChangeActivationResponse> ChangeActivation(LabelChangeActivationRequest request);
        Task<LabelCloneResponse> CloneLabel(LabelCloneRequest request);
        Task<LabelRestoreResponse> RestoreLabel(LabelRestoreRequest request);
        Task<LabelTranslationCreateResponse> CreateTranslation(LabelTranslationCreateRequest request);
        Task<LabelTranslationCreateListResponse> CreateTranslationFromList(LabelTranslationCreateListRequest request);
        Task<LabelTranslationReadListResponse> GetTranslation(LabelTranslationReadRequest request);
        Task<LabelTranslationReadListResponse> GetTranslations(LabelTranslationReadListRequest request);
        Task<LabelTranslationRevisionReadListResponse> GetLabelTranslationRevisions(
            LabelTranslationRevisionReadListRequest request);
        Task<LabelTranslationEditResponse> EditTranslation(LabelTranslationEditRequest request);
        Task<LabelTranslationDeleteResponse> DeleteTranslation(LabelTranslationDeleteRequest request);
        Task<LabelTranslationRestoreResponse> RestoreLabelTranslation(LabelTranslationRestoreRequest request);
    }
}