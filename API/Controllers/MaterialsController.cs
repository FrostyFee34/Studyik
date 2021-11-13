using Core.Entities;
using Core.Interfaces;

namespace API.Controllers
{
    public class MaterialsController : BaseApiController
    {
        private readonly IGenericRepository<Material> _repo;

        public MaterialsController(IGenericRepository<Material> repo)
        {
            _repo = repo;
        }
    }
}