using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN_Blog.Domain.Entities
{
    [NotMapped]
    public class PostComments
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public ICollection<CommentToPost> Comments { get; set; }
    }

    public class CommentToPost
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
