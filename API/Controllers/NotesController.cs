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
        public async Task<ActionResult<NoteDto>> Add(NoteDto noteDto)
        {
            var note = _mapper.Map<Note>(noteDto);
            note.UserUid ??= _currentUser.GetUid();

            var addedNote = await _repo.InsertAsync(note);
            if (addedNote == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500, "Database problems"));

            return Ok(_mapper.Map<NoteDto>(addedNote));
        }

        [HttpPut]
        public async Task<ActionResult<NoteDto>> Update(NoteDto noteDto)
        {
            var note = _mapper.Map<Note>(noteDto);
            note.UserUid ??= _currentUser.GetUid();

            var addedNote = await _repo.UpdateAsync(note);
            if (addedNote == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500, "Database problems"));

            return Ok(_mapper.Map<NoteDto>(addedNote));
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

        [HttpGet("material/{materialId}")]
        public async Task<ActionResult<IReadOnlyList<NoteDto>>> GetNotesByMaterialId(int materialId)
        {
            var userUid = _currentUser.GetUid();
            if (userUid == null) return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500));

            var spec = new NotesByUserUidAndMaterialIdSpec(userUid, materialId);
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

        [HttpDelete("{noteId}")]
        public async Task<ActionResult> DeleteMaterial(int noteId)
        {
            var userUid = _currentUser.GetUid();
            if (userUid == null) return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500));

            var spec = new NoteByUserUidAndNoteIdSpec(userUid, noteId);
            var material = await _repo.GetEntityWithSpecification(spec);
            if (material != null)
            {
                var result = await _repo.DeleteAsync(material);
                if (result > 0)
                    return Ok();
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500, "Database problems"));
            }

            return BadRequest(new ApiException(400));
        }
    }
}