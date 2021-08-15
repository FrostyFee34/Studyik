using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Task : BaseEntity
    {
        public string Header { get; set; }
        public string Text { get; set; }
        public List<Note> Notes { get; set; }
        public List<Video> Videos { get; set; }
        public List<Article> Articles { get; set; }
        public bool IsDone { get; set; }
        public DateTime ReminderTime { get; set; }
    }
}