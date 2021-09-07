using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.Specifications.Params;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class NotesController : BaseApiController
    {
        private readonly IGenericRepository<Note> _notesRepo;
        private readonly IMapper _mapper;
        public NotesController(IGenericRepository<Note> notesRepo, IMapper mapper)
        {
            _notesRepo = notesRepo;
            _mapper = mapper;
        }
        [HttpGet("Id")]
        public async Task<ActionResult<IReadOnlyList<Note>>> GetNote(int id)
        {
            var spec = new NotesWithSearchSpec(id);
            var notes = await _notesRepo.GetEntityWithSpecification(spec);
            return Ok(notes);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Note>>> GetNotes([FromQuery] NoteSpecParams noteParams)
        {
            var spec = new NotesWithSearchSpec(noteParams);
            var notes = await _notesRepo.ListAsync(spec);
            return Ok(notes);
        }
        [HttpPost]
        public async Task<ActionResult> InsertNote(NoteDTO obj)
        {
            var note = _mapper.Map<NoteDTO, Note>(obj);
            await _notesRepo.Insert(note);
            return new OkResult();
        }
        [HttpPut]
        public async Task<ActionResult> UpdateNote(NoteDTO obj)
        {
            var note = _mapper.Map<NoteDTO, Note>(obj);
            await _notesRepo.Update(note);
            return new OkResult();
        }
        
    }
}