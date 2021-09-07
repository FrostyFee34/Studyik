using System;

namespace API.DTO
{
    public class ArticleDTO
    {
        public int? Id { get; set;  }
        public string Header { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        public bool IsDone { get; set; }
        public DateTime? ReminderTime { get; set; }
        public int? JobId { get; set; }
    }
}