using Translation.Client.Web.Models.Language;
using Translation.Common.Models.DataTransferObjects;

namespace Translation.Client.Web.Helpers.Mappers
{
    public class LanguageMapper
    {
        public LanguageEditModel MapLanguageEditModel(LanguageDto dto)
        {
            var model = new LanguageEditModel();
            model.LanguageUid = dto.Uid;
            model.Name = dto.Name;
            model.OriginalName = dto.OriginalName;
            model.IsoCode2 = dto.IsoCode2;
            model.IsoCode3 = dto.IsoCode3;
            model.Description = dto.Description;

            model.SetInputModelValues();
            return model;
        }

        public LanguageDetailModel MapLanguageDetailModel(LanguageDto dto)
        {
            var model = new LanguageDetailModel();
            model.LanguageUid = dto.Uid;
            model.Name = dto.Name;
            model.OriginalName = dto.OriginalName;
            model.IsoCode2 = dto.IsoCode2;
            model.IsoCode3 = dto.IsoCode3;
            model.Description = dto.Description;

            model.SetInputModelValues();
            return model;
        }
    }
}