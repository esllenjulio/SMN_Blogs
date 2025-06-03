using SMN_Blog.Domain.Entities;
using SMN_Blog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN_Blog.Domain.Services
{
    public class PostService : IPostService
    {
        private IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<bool> Create(Post post)
        {
            await _postRepository.Create(post);
            return true;
        }

        public Task<bool> Delete(Post post)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _postRepository.Dispose();
        }

        public Task<bool> Update(Post post)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<PostResume>> GetPostsProcedureAsync()
        {
            return await _postRepository.GetPostsProcedureAsync();
        }

        public async Task<int> InsertPostProcedureAsync(string title, string content)
        {
            return await _postRepository.InsertPostProcedureAsync(title, content);
        }
        public async Task<PostComments> FindIdPostProcedureAsync(int id)
        {
            return await _postRepository.FindIdPostProcedureAsync(id);
        }

        public async Task<int> CreateCommentToPostProcedureAsync(int postId, string Description)
        {
            return await _postRepository.CreateCommentToPostProcedureAsync(postId, Description);
        }
    }
}
