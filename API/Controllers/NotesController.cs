using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Errors;
using API.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class NotesController : BaseApiController
    {
        private readonly UserResolverService _currentUser;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Note> _repo;

        public NotesController(IGenericRepository<Note> repo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _mapper = mapper;
            _currentUser = new UserResolverService(httpContextAccessor);
        }


        [HttpPost]
        public async Task<ActionResult> Add(NoteDto noteDto)
        {
            var note = _mapper.Map<Note>(noteDto);
            note.UserUid ??= _currentUser.GetUid();

            var isFinished = await _repo.InsertAsync(note);
            if (isFinished <= 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500, "Database problems"));


            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(NoteDto noteDto)
        {
            var note = _mapper.Map<Note>(noteDto);
            note.UserUid ??= _currentUser.GetUid();

            var isFinished = await _repo.UpdateAsync(note);
            if (isFinished <= 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500, "Database problems"));

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<NoteDto>>> GetNotes()
        {
            var userUid = _currentUser.GetUid();
            if (userUid == null) return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500));

            var spec = new NotesByUserUidSpec(userUid);
            var notes = await _repo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<NoteDto>>(notes));
        }

        [HttpGet("{noteId}")]
        public async Task<ActionResult<NoteDto>> GetNote(int noteId)
        {
            var userUid = _currentUser.GetUid();
            if (userUid == null) return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500));

            var spec = new NoteByUserUidAndNoteIdSpec(userUid, noteId);

            var note = await _repo.GetEntityWithSpecification(spec);

            return Ok(_mapper.Map<NoteDto>(note));
        }
    }
}