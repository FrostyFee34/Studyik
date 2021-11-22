using Core.Entities;

namespace Core.Specifications
{
    public class NoteByUserUidAndNoteIdSpec : BaseSpecification<Note>
    {
        public NoteByUserUidAndNoteIdSpec(string userUid, int id) : base(n => n.UserUid == userUid && n.Id == id)
        {
        }
    }
}