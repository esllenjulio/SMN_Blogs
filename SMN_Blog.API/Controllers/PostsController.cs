using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMN_Blog.API.DTO;
using SMN_Blog.Domain.Entities;
using SMN_Blog.Domain.Interfaces;
using SMN_Blog.Domain.Services;
using SMN_Blog.Infrastructure.Repository;
using System.Threading.Tasks;

namespace SMN_Blog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Tags("Controllers Via - Repository")]
    public class PostsController : ControllerBase
    {
        private IPostRepository _postRepository;
        private IPostService _postService;
        private ICommentService _commentService;
        private ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public PostsController(IPostRepository postRepository, ICommentRepository commentRepository, IPostService postService, ICommentService commentService, IMapper mapper)
        {
            _postRepository = postRepository;
            _postService = postService;
            _commentService = commentService;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostResumeDto>>> Get()
        {
            var posts = await _postRepository.GetAll();
            if (posts == null || !posts.Any())
                return NoContent();

            return Ok(_mapper.Map<IEnumerable<PostResumeDto>>(posts));
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> Create(CreateOrUpdatePostDto postDto)
        {
            try
            {
                var post = _mapper.Map<Post>(postDto);
                var created = await _postService.Create(post);

                if (!created)
                    return BadRequest("Não foi possível criar o post.");

                var postDtoResult = _mapper.Map<PostDto>(post);
                return CreatedAtAction(nameof(FindToId), new { id = postDtoResult.Id }, postDtoResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar post: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> FindToId(int id)
        {
            var post = await _postRepository.FindAsync(id);

            if (post == null)
                return NotFound($"Post com ID {id} não encontrado.");

            return Ok(_mapper.Map<PostDto>(post));
        }

        [HttpPost("{id}/comments")]
        public async Task<ActionResult<CommentDto>> CreateComment(int id, [FromBody] CreateCommentDto commentRequest)
        {
            try
            {
                if (commentRequest == null || string.IsNullOrWhiteSpace(commentRequest.Description))
                    return BadRequest("Descrição obrigatória");

                var post = await _postRepository.FindAsync(id);
                if (post == null)
                    return NotFound($"Post com ID {id} não encontrado.");

                var comment = new Comment
                {
                    Description = commentRequest.Description,
                    PostId = id
                };

                var created = await _commentService.Create(comment);

                if (!created)
                    return BadRequest("Não foi possível adicionar o comentário.");

                var commentDto = _mapper.Map<CommentDto>(comment);
                return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, commentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao adicionar comentário: {ex.Message}");
            }
        }

        // Suporte para CreatedAtAction
        [HttpGet("comments/{id}")]
        public async Task<ActionResult<CommentDto>> GetCommentById(int id)
        {
            var comment = await _commentRepository.FindAsync(id);
            if (comment == null)
                return NotFound();

            return Ok(_mapper.Map<CommentDto>(comment));
        }

    }
}
