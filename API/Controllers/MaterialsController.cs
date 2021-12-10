using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTO;
using API.Errors;
using API.Services;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.Specifications.Params;
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
        public async Task<ActionResult<MaterialToReturnDto>> Add(MaterialToInsertDto materialDto)
        {
            var material = _mapper.Map<Material>(materialDto);
            material.UserUid ??= _currentUser.GetUid();
            
            var addedMaterial = await _repo.InsertAsync(material);
            if (addedMaterial == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500, "Database problems"));

            return Ok(_mapper.Map<MaterialToReturnDto>(addedMaterial));
        }

        [HttpPut]
        public async Task<ActionResult<MaterialToReturnDto>> Update(MaterialToInsertDto materialDto)
        {
            var material = _mapper.Map<Material>(materialDto);
            material.UserUid ??= _currentUser.GetUid();

            var addedMaterial = await _repo.UpdateAsync(material);
            if (addedMaterial == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500, "Database problems"));

            return Ok(_mapper.Map<MaterialToReturnDto>(addedMaterial));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<MaterialToReturnDto>>> GetMaterials(
            [FromQuery] MaterialsSpecParams specParams)
        {   
            var userUid = _currentUser.GetUid();
            if (userUid == null) return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500));

            var spec = new MaterialsByUserUidAndParamsSpec(specParams, userUid);
            var materials = await _repo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<MaterialToReturnDto>>(materials));
        }

        [HttpGet("{materialId}")]
        public async Task<ActionResult<IReadOnlyList<MaterialToReturnDto>>> GetMaterial(int materialId)
        {
            var userUid = _currentUser.GetUid();
            if (userUid == null) return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500));

            var spec = new MaterialByUserUidAndMaterialId(userUid, materialId);
            var material = await _repo.GetEntityWithSpecification(spec);

            return Ok(_mapper.Map<MaterialToReturnDto>(material));
        }

        [HttpDelete("{materialId}")]
        public async Task<ActionResult> DeleteMaterial(int materialId)
        {
            var userUid = _currentUser.GetUid();
            if (userUid == null) return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500));

            var spec = new MaterialByUserUidAndMaterialId(userUid, materialId);
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