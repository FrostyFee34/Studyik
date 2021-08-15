using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class NotesController : BaseApiController
    {
        private readonly GenericRepository<Note> _notesRepo;

        public NotesController(GenericRepository<Note> notesRepo)
        {
            _notesRepo = notesRepo;
        }

        [HttpGet]
        public Task<ActionResult<IReadOnlyList<Note>>> GetNotes()
        {

        }
    }
}