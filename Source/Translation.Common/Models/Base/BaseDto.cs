using System;

namespace Translation.Common.Models.Base
{
    public class BaseDto
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}