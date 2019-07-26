using System;

using Translation.Client.Web.Models.Base;
using Translation.Client.Web.Models.InputModels;

namespace Translation.Client.Web.Models.Organization
{
    public sealed class OrganizationDetailModel : BaseModel
    {
        public Guid OrganizationUid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                IsActiveInput.Value = _isActive;
            }
        }

        public CheckboxInputModel IsActiveInput { get; set; }

        public OrganizationDetailModel()
        {
            Title = "organization_detail_title";

            IsActiveInput = new CheckboxInputModel("IsActive", "is_active", false, true, false);
        }
    }
}