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
    public class GroupsController : BaseApiController
    {
        private readonly UserResolverService _currentUser;
        private readonly IGenericRepository<Group> _repo;
        private readonly IMapper _mapper;

        public GroupsController(IGenericRepository<Group> repo, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _currentUser = new UserResolverService(contextAccessor);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Group>>> GetNotes()
        {
            var userUid = _currentUser.GetUid();
            if (userUid == null) return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500));

            var spec = new GroupsByUserIdSpec(userUid);
            var groups = await _repo.ListAsync(spec);

            return Ok(groups);
        }

        [HttpPost]
        public async Task<ActionResult<Group>> Add(Group group)
        {
            group.UserUid ??= _currentUser.GetUid();

            var addedGroup = await _repo.InsertAsync(group);
            if (addedGroup == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500, "Database problems"));

            return Ok(addedGroup);
        }

        [HttpPut]
        public async Task<ActionResult<Group>> Update(Group group)
        {
            group.UserUid ??= _currentUser.GetUid();

            var addedGroup = await _repo.UpdateAsync(group);
            if (addedGroup == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500, "Database problems"));

            return Ok(addedGroup);
        }
    }
}