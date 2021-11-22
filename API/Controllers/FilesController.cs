using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
    public class FilesController : BaseApiController
    {
        private readonly UserResolverService _currentUser;
        private readonly IGenericRepository<Material> _repo;

        public FilesController(IGenericRepository<Material> repo, IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _currentUser = new UserResolverService(httpContextAccessor);
        }

        [HttpPut("{materialId}")]
        [DisableRequestSizeLimit]
        public async Task<ActionResult> AddFile(int materialId)
        {
            // Get a corresponding material and check if it's category supports file
            var userUid = _currentUser.GetUid();
            if (userUid == null) return StatusCode(StatusCodes.Status500InternalServerError, new ApiException(500));

            var spec = new MaterialByUserUidAndMaterialId(userUid, materialId);
            var material = await _repo.GetEntityWithSpecification(spec);
            if (material == null || material.Category.Name != "File")
                return BadRequest(new ApiException(400, @"You cannot add a file to this material"));

            try
            {
                // Get a file out of request form
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();

                var folderName = Path.Combine("Resources", "Files");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    // Get a file name and hash it
                    var fileName = file.FileName.Trim('"');
                    if (material.Link == null)
                    {
                        using var hash = SHA256.Create();
                        var byteArray = hash.ComputeHash(Encoding.UTF8.GetBytes(fileName + DateTime.Now + userUid));
                        fileName = $"{Convert.ToHexString(byteArray).ToLower()}{Path.GetExtension(fileName)}";
                    }

                    // Save a file to drive
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    await using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Update material
                    material.Link = dbPath;
                    var isFinished = await _repo.UpdateAsync(material);
                    if (isFinished <= 0)
                        return StatusCode(StatusCodes.Status500InternalServerError,
                            new ApiException(500, "Database problems"));

                    return Ok();
                }

                return BadRequest(new ApiException(400, "File is empty"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiException(500, "Error while saving a file", ex.StackTrace));
            }
        }
    }
}