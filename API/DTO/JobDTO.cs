using System;

namespace API.DTO
{
    public class JobDTO
    {
        public int? Id { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public bool IsDone { get; set; }
        public DateTime? ReminderTime { get; set; }
    }
}