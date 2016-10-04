using System;

namespace rich.Models
{
    public class Event : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int EventType { get; set; }
    }
}
