using Core.Entities;

namespace Core.Specifications
{
    public class NotesByUserUidSpec : BaseSpecification<Note> 
    {
        public NotesByUserUidSpec(string userUid) : base(n => n.UserUid == userUid)
        {
        }
    }
}