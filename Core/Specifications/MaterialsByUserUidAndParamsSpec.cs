using Core.Entities;
using Core.Specifications.Params;

namespace Core.Specifications
{
    public class MaterialsByUserUidAndParamsSpec : BaseSpecification<Material>
    {
        public MaterialsByUserUidAndParamsSpec(MaterialsSpecParams specParams, string userUid) : base(m =>
            m.UserUid == userUid &&
            (string.IsNullOrEmpty(specParams.Search) || m.Title.ToLower().Contains(specParams.Search)) &&
            (!specParams.CategoryId.HasValue || m.CategoryId == specParams.CategoryId) &&
            (!specParams.GroupId.HasValue || m.GroupId == specParams.GroupId))
        {
            AddInclude(o => o.Category);
            AddInclude(o => o.Group);

            switch (specParams.Sort)
            {
                case "dateDesc":
                    AddOrderByDescending(p => p.CreationDate);
                    break;
                case "dateAsc":
                    AddOrderBy(p => p.CreationDate);
                    break;  
                case "alphabetical":
                    AddOrderBy(n => n.Title);
                    break;
                default:
                    AddOrderBy(i => i.Id);
                    break;
            }
        }
    }
}