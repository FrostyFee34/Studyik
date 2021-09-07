using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Job : BaseEntity
    {
        public string Header { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public DateTime? ReminderTime { get; set; }

        public ICollection<Article> Articles { get; set; }
        public ICollection<Note> Notes { get; set; }
        public ICollection<Video> Videos { get; set; }
    }
}