using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Authorize]
    public class GroupsController : BaseApiController
    {
        private readonly IGenericRepository<Group> _repo;

        public GroupsController(IGenericRepository<Group> repo)
        {
            _repo = repo;
        }
    }
}