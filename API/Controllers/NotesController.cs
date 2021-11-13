using Core.Entities;
using Core.Interfaces;

namespace API.Controllers
{
    public class NotesController : BaseApiController
    {
        private readonly IGenericRepository<Note> _repo;

        public NotesController(IGenericRepository<Note> repo)
        {
            _repo = repo;
        }
    }
}