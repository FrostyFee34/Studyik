using Core.Entities;

namespace Core.Specifications
{
    public class MaterialsByUserUidSpec : BaseSpecification<Material>
    {
        public MaterialsByUserUidSpec(string userUid) : base(m => m.UserUid == userUid)
        {
            AddInclude(o=>o.Category);
            AddInclude(o=>o.Group);
        }
        
    }
}