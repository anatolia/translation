using System;

using Translation.Common.Models.Base;

namespace Translation.Common.Models.DataTransferObjects
{
    public class RevisionDto<T> where T : BaseDto
    {
        public int Revision { get; set; }
        public Guid RevisionedByUid { get; set; }
        public string RevisionedByName { get; set; }
        public DateTime RevisionedAt { get; set; }

        public T Item { get; set; }
    }
}