using Microsoft.EntityFrameworkCore;
using SMN_Blog.Domain.Entities;
using SMN_Blog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SMN_Blog.Infrastructure.Repository
{
    public class CommentRepository : EFRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BlogDbContext context) : base(context)
        {
        }

        
    }
}
