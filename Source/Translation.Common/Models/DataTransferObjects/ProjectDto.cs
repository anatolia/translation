using System;

using Translation.Common.Models.Base;

namespace Translation.Common.Models.DataTransferObjects
{
    public class ProjectDto : BaseDto
    {
        public Guid OrganizationUid { get; set; }
        public string OrganizationName { get; set; }

        public string Url { get; set; }
        public string Description { get; set; }
        public int LabelCount { get; set; }
        public bool IsActive { get; set; }
    }
}