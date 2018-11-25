using System;

using Cheviri.Data.Entities.Base;
using Cheviri.Data.Entities.Base.Schemas;
using Cheviri.Data.Entities.Project;

namespace Cheviri.Data.Entities.Main
{
    public class Token : BaseEntity, ISchemaMain
    {
        public Organization Organization { get; set; }
        public Integration Integration { get; set; }

        public string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}