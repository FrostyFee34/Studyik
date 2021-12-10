using Core.Entities;

namespace Core.Specifications
{
    public class NotesByUserUidAndMaterialIdSpec : BaseSpecification<Note>
    {
        public NotesByUserUidAndMaterialIdSpec(string userUid, int materialId) : base(n => n.UserUid == userUid && n.MaterialId == materialId)
        {
        }   
    }
}