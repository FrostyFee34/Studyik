using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Article : BaseEntity
    {
        public string Header { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        public List<Note> Notes { get; set; }
        public bool IsDone { get; set; }
        public DateTime ReminderTime { get; set; }

    }
}