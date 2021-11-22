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
    public class MaterialsController : BaseApiController
    {
        private readonly UserResolverService _currentUser;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Material> _repo;

        public MaterialsController(IGenericRepository<Material> repo, IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _currentUser = new UserResolverService(httpContextAccessor);
        }


        [HttpPost]
        public async Task<ActionResult> Add(MaterialToInsertDto materialDto)
        {
            var material = _mapper.Map<Material>(materialDto);
            material.UserUid ??= _currentUser.GetUid();

            var isFinished = await _repo.InsertAsync(material);
            if (isFinished <= 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500, "Database problems"));

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(MaterialToInsertDto materialDto)
        {
            var material = _mapper.Map<Material>(materialDto);
            material.UserUid ??= _currentUser.GetUid();

            var isFinished = await _repo.UpdateAsync(material);
            if (isFinished <= 0)
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500, "Database problems"));

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<MateriaToReturnlDto>>> GetMaterials()
        {
            var userUid = _currentUser.GetUid();
            if (userUid == null) return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500));

            var spec = new MaterialsByUserUidSpec(userUid);
            var materials = await _repo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<MateriaToReturnlDto>>(materials));
        }

        [HttpGet("{materialId}")]
        public async Task<ActionResult<IReadOnlyList<MateriaToReturnlDto>>> GetMaterial(int materialId)
        {
            var userUid = _currentUser.GetUid();
            if (userUid == null) return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500));

            var spec = new MaterialByUserUidAndMaterialId(userUid, materialId);
            var materials = await _repo.GetEntityWithSpecification(spec);

            return Ok(_mapper.Map<MateriaToReturnlDto>(materials));
        }
    }
}