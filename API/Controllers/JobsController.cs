using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTO;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Core.Specifications.Params;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class JobsController : BaseApiController
    {
        private readonly IGenericRepository<Job> _jobsRepo;
        private readonly IMapper _mapper;

        public JobsController(IGenericRepository<Job> jobsRepo, IMapper mapper)
        {
            _jobsRepo = jobsRepo;
            _mapper = mapper;
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<Job>>> GetJob(int id)
        {
            var spec = new JobsWithArticlesNotesAndVideosSpec(id);
            var job = await _jobsRepo.GetEntityWithSpecification(spec);
            if (job == null) return NotFound(new ApiResponse(404));
            return Ok(job);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Job>>> GetJobs([FromQuery] JobSpecParams jobParams)
        {
            var spec = new JobsWithArticlesNotesAndVideosSpec(jobParams);
            var jobs = await _jobsRepo.ListAsync(spec);

            return Ok(jobs);
        }

        [HttpPost]
        public async Task<ActionResult> InsertJob(JobDTO obj)
        {
            var job = _mapper.Map<JobDTO, Job>(obj);
            await _jobsRepo.Insert(job);
            return new OkResult();
        }
        [HttpPut]
        public async Task<ActionResult> Update(JobDTO obj)
        {
            var job = _mapper.Map<JobDTO, Job>(obj);
            await _jobsRepo.Update(job);
            return new OkResult();
        }
    }
}