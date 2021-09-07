namespace API.DTO
{
    public class NoteDTO
    {
        public int? Id { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public int? Timestamp { get; set; }
        public int? ArticleId { get; set; }
        public int? JobId { get; set; }
        public int? VideoId { get; set; }

    }
}