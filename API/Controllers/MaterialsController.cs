using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace API.Controllers
{
    [Authorize]
    public class MaterialsController : BaseApiController
    {
        private readonly IGenericRepository<Material> _repo;

        public MaterialsController(IGenericRepository<Material> repo)
        {
            _repo = repo;
        }
      
    }
}