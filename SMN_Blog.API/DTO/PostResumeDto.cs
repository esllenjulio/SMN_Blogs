using SMN_Blog.Domain.Entities;

namespace SMN_Blog.API.DTO
{
    public class PostResumeDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int CountComments { get; set; }
    }
}
