namespace Core.Entities
{
    public class Note : BaseEntity
    {
        public string Header { get; set; }
        public string Text { get; set; }
        public int Timestamp { get; set; }

    }
}