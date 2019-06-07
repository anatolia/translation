using System;

using Translation.Data.Entities.Base;
using Translation.Data.Entities.Base.Schemas;
using Translation.Data.Entities.Project;

namespace Translation.Data.Entities.Main
{
    public class Token : BaseEntity, ISchemaMain
    {
        public Organization Organization { get; set; }
        public Integration Integration { get; set; }

        public string AccessToken { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}