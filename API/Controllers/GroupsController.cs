using System.Collections.Generic;
using System.Threading.Tasks;
using API.Errors;
using API.Services;
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

        public GroupsController(IGenericRepository<Group> repo, IHttpContextAccessor contextAccessor)
        {
            _repo = repo;
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
        public async Task<ActionResult> Add(Group group)
        {
            group.UserUid ??= _currentUser.GetUid();

            var isFinished = await _repo.InsertAsync(group);
            if (isFinished <= 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500, "Database problems"));


            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(Group group)
        {
            group.UserUid ??= _currentUser.GetUid();

            var isFinished = await _repo.UpdateAsync(group);
            if (isFinished <= 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500, "Database problems"));


            return Ok();
        }
    }
}