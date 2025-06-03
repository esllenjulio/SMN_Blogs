using SMN_Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN_Blog.Domain.Interfaces
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<List<PostResume>> GetPostsProcedureAsync();
        Task<int> InsertPostProcedureAsync(string title, string content);
        Task<PostComments> FindIdPostProcedureAsync(int id);
        Task<int> CreateCommentToPostProcedureAsync(int postId, string Description);
    }
}
