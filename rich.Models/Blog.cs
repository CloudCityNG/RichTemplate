using System;

namespace rich.Models
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int BlogType { get; set; }
    }
}
