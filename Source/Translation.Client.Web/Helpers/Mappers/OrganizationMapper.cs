using Translation.Client.Web.Models.Organization;
using Translation.Common.Models.DataTransferObjects;

namespace Translation.Client.Web.Helpers.Mappers
{
    public class OrganizationMapper
    {
        public OrganizationDetailModel MapOrganizationDetailModel(OrganizationDto dto)
        {
            var model = new OrganizationDetailModel();

            model.OrganizationUid = dto.Uid;
            model.Name = dto.Name;
            model.Description = dto.Description;
            model.IsActive = dto.IsActive;

            model.SetInputModelValues();

            return model;
        }

        public OrganizationEditModel MapOrganizationEditModel(OrganizationDto dto)
        {
            var model = new OrganizationEditModel();

            model.OrganizationUid = dto.Uid;
            model.Name = dto.Name;
            model.Description = dto.Description;
            model.SetInputModelValues();

            return model;
        }
    }
}