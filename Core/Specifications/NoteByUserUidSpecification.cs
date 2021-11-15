using Core.Entities;

namespace Core.Specifications
{
    public class NoteByUserUidSpecification : BaseSpecification<Note> 
    {
        public NoteByUserUidSpecification(string userUid) : base(n => n.UserUid == userUid)
        {
        }
    }
}