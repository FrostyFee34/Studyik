using System;

namespace API.DTO
{
    public class MaterialToInsertDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int? GroupId { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}