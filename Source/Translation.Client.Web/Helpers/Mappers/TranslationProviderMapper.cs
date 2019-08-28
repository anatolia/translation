using System;

using Translation.Client.Web.Models.TranslationProvider;
using Translation.Common.Models.DataTransferObjects;


namespace Translation.Client.Web.Helpers.Mappers
{
    public class TranslationProviderMapper
    {
        public static TranslationProviderEditModel MapTranslationProviderEditModel(
                   TranslationProviderDto translationProvider)
        {
            var model = new TranslationProviderEditModel();

            model.TranslationProviderUid = translationProvider.Uid;
            model.Value = translationProvider.Value;
            model.Name = translationProvider.Name;
            model.Description = translationProvider.Description;

            model.SetInputModelValues();
            return model;
        }


        public static TranslationProviderDetailModel MapTranslationProviderDetailModel(TranslationProviderDto dto)
        {
            var model = new TranslationProviderDetailModel();
            model.TranslationProviderUid = dto.Uid;
            model.Value = dto.Value;
            model.Name = dto.Name;
            model.Description = dto.Description;

            return model;
        }

    }
}