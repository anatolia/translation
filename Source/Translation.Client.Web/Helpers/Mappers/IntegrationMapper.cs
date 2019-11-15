using System;

using Translation.Client.Web.Models.Integration;
using Translation.Common.Models.DataTransferObjects;

namespace Translation.Client.Web.Helpers.Mappers
{
    public class IntegrationMapper
    {
        public IntegrationCreateModel MapIntegrationCreateModel(Guid organizationUid)
        {
            var model = new IntegrationCreateModel();
            model.OrganizationUid = organizationUid;

            model.SetInputModelValues();

            return model;
        }

        public IntegrationDetailModel MapIntegrationDetailModel(IntegrationDto dto)
        {
            var model = new IntegrationDetailModel();
            model.OrganizationUid = dto.OrganizationUid;
            model.OrganizationName = dto.OrganizationName;

            model.IntegrationUid = dto.Uid;
            model.Name = dto.Name;
            model.Description = dto.Description;

            return model;
        }

        public IntegrationClientActiveTokensModel MapIntegrationClientActiveTokensModel(IntegrationClientDto dto)
        {
            var model = new IntegrationClientActiveTokensModel();
            model.IntegrationUid = dto.IntegrationUid;
            model.IntegrationName = dto.IntegrationName;
            model.ClientUid = dto.Uid;

            return model;
        }

        public IntegrationActiveTokensModel MapIntegrationActiveTokensModel(IntegrationDto dto)
        {
            var model = new IntegrationActiveTokensModel();
            model.IntegrationUid = dto.Uid;
            model.IntegrationName = dto.Name;

            return model;
        }

        public IntegrationEditModel MapIntegrationEditModel(IntegrationDto dto)
        {
            var model = new IntegrationEditModel();
            model.IntegrationUid = dto.Uid;
            model.Name = dto.Name;
            model.Description = dto.Description;
            model.SetInputModelValues();

            return model;
        }

        public IntegrationClientTokenRequestLogsModel MapIntegrationClientTokenRequestLogsModel(Guid id)
        {
            var model = new IntegrationClientTokenRequestLogsModel();
            model.IntegrationClientUid = id;

            return model;
        }
    }
}