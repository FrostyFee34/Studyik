using System;

namespace Core.Entities
{
    public class Material : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public Group Group { get; set; }
        public int? GroupId { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string UserUid { get; set; }
        public DateTime CreationDate { get; set; }
    }
}