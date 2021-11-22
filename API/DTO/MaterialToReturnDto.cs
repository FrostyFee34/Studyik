﻿using Core.Entities;

namespace API.DTO
{
    public class MaterialDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public string Group { get; set; }
        public int? GroupId { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
    }
}