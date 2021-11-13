using Core.Entities;
using Core.Interfaces;

namespace API.Controllers
{
    public class GroupsController : BaseApiController
    {
        private readonly IGenericRepository<Group> _repo;

        public GroupsController(IGenericRepository<Group> repo)
        {
            _repo = repo;
        }
    }
}