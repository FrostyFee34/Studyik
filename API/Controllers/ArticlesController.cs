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
    public class ArticlesController : BaseApiController
    {
        private readonly IGenericRepository<Article> _articlesRepo;
        private readonly IMapper _mapper;
        public ArticlesController(IGenericRepository<Article> articlesRepo, IMapper mapper)
        {
            _articlesRepo = articlesRepo;
            _mapper = mapper;
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<IReadOnlyList<Article>>> GetArticle(int id)
        {
            var spec = new ArticlesWithNotesSpec(id);
            var articles = await _articlesRepo.GetEntityWithSpecification(spec);
            return Ok(articles);
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Article>>> GetArticles([FromQuery] ArticleSpecParams articleParams)
        {
            var spec = new ArticlesWithNotesSpec(articleParams);
            var articles = await _articlesRepo.ListAsync(spec);
            return Ok(articles);
        }
        [HttpPost]
        public async Task<ActionResult> InsertArticle(ArticleDTO obj)
        {

            var article = _mapper.Map<ArticleDTO, Article>(obj);
            await _articlesRepo.Insert(article);
            return new OkResult();
        }
        [HttpPut]
        public async Task<ActionResult> UpdateArticle(ArticleDTO obj)
        {

            var article = _mapper.Map<ArticleDTO, Article>(obj);
            await _articlesRepo.Update(article);
            return new OkResult();
        }
    }
}