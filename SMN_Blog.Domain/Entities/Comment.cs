using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN_Blog.Domain.Entities
{
    public class Comment: BaseEntity
    {
        public string Description { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
