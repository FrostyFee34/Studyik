namespace API.DTO
{
    public class NoteDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public uint? StartIndex { get; set; }
        public uint? EndIndex { get; set; }
        public int? MaterialId { get; set; }
        public string UserUid { get; set; }
    }
}