using SMN_Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN_Blog.Domain.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
       
    }
}
