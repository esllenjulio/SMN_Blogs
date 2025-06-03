using SMN_Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN_Blog.Domain.Interfaces
{
    public interface IPostService : IDisposable
    {
        Task<bool> Create(Post post);
        Task<bool> Update(Post post);
        Task<bool> Delete(Post post);
        Task<IList<PostResume>> GetPostsProcedureAsync();
        Task<int> InsertPostProcedureAsync(string title, string content);
        Task<PostComments> FindIdPostProcedureAsync(int id);
        Task<int> CreateCommentToPostProcedureAsync(int postId, string Description);
    }
}
