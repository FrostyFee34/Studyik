using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private readonly IGenericRepository<Category> _repo;

        public CategoriesController(IGenericRepository<Category> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Category>>> Get()
        {
            return Ok(await _repo.ListAllAsync());
        }
    }
}