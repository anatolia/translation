using System;

using NodaTime;

namespace Translation.Common.Models.Base
{
    public class BaseDto
    {
        public Guid Uid { get; set; }
        public string Name { get; set; }

        public Instant CreatedAt { get; set; }
        public Instant? UpdatedAt { get; set; }
    }
}