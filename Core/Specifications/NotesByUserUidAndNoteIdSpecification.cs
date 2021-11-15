using Core.Entities;

namespace Core.Specifications
{
    public class NotesByUserUidAndNoteIdSpecification : BaseSpecification<Note>
    {
        public NotesByUserUidAndNoteIdSpecification(string userUid, int id) : base(n => n.UserUid == userUid && n.Id == id)
        {
        }
    }
}