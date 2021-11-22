using Core.Entities;

namespace Core.Specifications
{
    public class MaterialByUserUidAndMaterialId : BaseSpecification<Material>
    {
        public MaterialByUserUidAndMaterialId(string userUid, int id) : base(m => m.UserUid == userUid && m.Id == id)
        {
            AddInclude(o=>o.Category);
            AddInclude(o=>o.Group);
        }
    }
}