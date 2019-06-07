using System;

namespace Translation.Data.Entities.Base
{
    public class BaseRevision<T> where T : BaseEntity
    {
        public long Id { get; set; }
        public long Revision { get; set; }
        public long RevisionedBy { get; set; }
        public DateTime RevisionedAt { get; set; }

        public T Entity { get; set; }
    }
}