using SMN_Blog.Domain.Entities;
using SMN_Blog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMN_Blog.Domain.Services
{
    public class CommentService : ICommentService
    {
        private ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<bool> Create(Comment comment)
        {
            await _commentRepository.Create(comment);
            return true;
        }

        public Task<bool> Delete(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _commentRepository.Dispose();
        }

        
    }
}
