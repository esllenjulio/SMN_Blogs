using SMN_Blog.Domain.Entities;

namespace SMN_Blog.API.DTO
{
    public class PostDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}
