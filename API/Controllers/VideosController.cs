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
    public class VideosController : BaseApiController
    {
        private readonly IGenericRepository<Video> _videosRepo;
        private readonly IMapper _mapper;

        public VideosController(IGenericRepository<Video> videosRepo, IMapper mapper)
        {
            _videosRepo = videosRepo;
            _mapper = mapper;
        }
        [HttpGet("Id")]
        public async Task<ActionResult<IReadOnlyList<Video>>> GetVideo(int id)
        {
            var spec = new VideosWithNotesSpec(id);
            var videos = await _videosRepo.GetEntityWithSpecification(spec);
            return Ok(videos);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Video>>> GetVideos([FromQuery] VideoSpecParams videoParams)
        {
            var spec = new VideosWithNotesSpec(videoParams);
            var videos = await _videosRepo.ListAsync(spec);
            return Ok(videos);
        }
        [HttpPost]
        public async Task<ActionResult> InsertVideo(VideoDTO obj)
        {
            var video = _mapper.Map<VideoDTO, Video>(obj);
            await _videosRepo.Insert(video);
            return new OkResult();
        }
        [HttpPut]
        public async Task<ActionResult> UpdateVideo(VideoDTO obj)
        {
            var video = _mapper.Map<VideoDTO, Video>(obj);
            await _videosRepo.Update(video);
            return new OkResult();
        }
        
    }
}