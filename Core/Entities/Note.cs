using System;

namespace Core.Entities
{
    public class Note : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public uint? StartIndex { get; set; }
        public uint? EndIndex { get; set; }
        public Material Material { get; set; }
        public int? MaterialId { get; set; }
        public string UserUid { get; set; }
        public DateTime CreationDate { get; set; }
    }
}