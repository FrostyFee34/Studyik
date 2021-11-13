using Core.Entities;
using Core.Interfaces;

namespace API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private readonly IGenericRepository<Category> _repo;

        public CategoriesController(IGenericRepository<Category> repo)
        {
            _repo = repo;
        }


    }
}