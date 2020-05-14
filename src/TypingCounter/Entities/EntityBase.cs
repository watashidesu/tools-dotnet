using System;

namespace TypingCounter.Entities
{
    public abstract class EntityBase
    {
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
    }
}
