using System;
using Translation.Common.Models.Base;

namespace Translation.Common.Models.DataTransferObjects
{
    public class LabelDto : BaseDto
    {
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public Guid ProjectUid { get; set; }
        public string ProjectName { get; set; }

        public string Key { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}