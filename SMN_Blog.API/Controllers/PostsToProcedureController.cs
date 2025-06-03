using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMN_Blog.API.DTO;
using SMN_Blog.Domain.Entities;
using SMN_Blog.Domain.Interfaces;
using SMN_Blog.Domain.Services;
using SMN_Blog.Infrastructure;
using SMN_Blog.Infrastructure.Repository;

namespace SMN_Blog.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Controllers Via - Procedures")]
    public class PostsToProcedureController : ControllerBase
    {

        private IPostService _postService;
        private readonly IMapper _mapper;

        public PostsToProcedureController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        /// <summary>Buscar todos os posts</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostResumeDto>>> GetAll()
        {
            //var posts = await _context.Post
            //    .FromSqlRaw("EXEC sp_GetPostsWithComments")
            //    .ToListAsync();

            var posts = await _postService.GetPostsProcedureAsync();
            if (posts == null || !posts.Any())
                return NoContent();
            var allPostsResume = _mapper.Map<IEnumerable<PostResumeDto>>(posts);
            return Ok(allPostsResume);
        }

        /// <summary>Busca post por ID</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetById(int id)
        {
            //var post = await _context.PostsDto
            //    .FromSqlRaw("EXEC sp_GetPostById @Id = {0}", id)
            //    .FirstOrDefaultAsync();

            var post = await _postService.FindIdPostProcedureAsync(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        /// <summary>Criar um novo post</summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdatePostDto post)
        {
            //var id = await _context.Database.ExecuteSqlRawAsync(
            //    "EXEC sp_CreatePost @Title = {0}, @Content = {1}",
            //    dto.Title, dto.Content
            //);
          
            try
            {
                var created = _postService.InsertPostProcedureAsync(post.Title, post.Content);

                if (created.Result == 0 )
                    return BadRequest("Não foi possível criar o post.");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar post: {ex.Message}");
            }
        }

        /// <summary>Adicionar comentário a um post</summary>
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> AddComment(int id, [FromBody] CreateCommentDto commentRequest)
        {
            //await _context.Database.ExecuteSqlRawAsync(
            //    "EXECUTE sp_AddComment @PostId = {0}, @Description = {1}",
            //    id, commentRequest.Description
            //);

            if (commentRequest == null || string.IsNullOrWhiteSpace(commentRequest.Description))
                return BadRequest("Descrição obrigatória");

            try
            {
                var created = await _postService.CreateCommentToPostProcedureAsync(id, commentRequest.Description);

                if (created == 0)
                    return BadRequest("Não foi possível adicionar o comentário.");

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar comentário: {ex.Message}");
            }
        }

    }
}
