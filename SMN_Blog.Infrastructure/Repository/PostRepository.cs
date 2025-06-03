using Microsoft.EntityFrameworkCore;
using SMN_Blog.Domain.Entities;
using SMN_Blog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SMN_Blog.Infrastructure.Repository
{
    public class PostRepository : EFRepository<Post>, IPostRepository
    {
        public PostRepository(BlogDbContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Post.AsNoTracking()
                .Include(t => t.Comments)
                .ToListAsync();
        }

        public override async Task<Post> FindAsync(int Id)
        {
            return await _context.Post
                                .AsNoTracking()
                                .Include(p => p.Comments)
                                .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<List<PostResume>> GetPostsProcedureAsync()
        {
            #region Consulta Post Resumo
            try
            {
                var result = new List<PostResume>();

                using (var connection = _context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_GetAllPosts";
                        command.CommandType = CommandType.StoredProcedure;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                result.Add(new PostResume
                                {
                                    Id = reader.GetInt32(0),
                                    Title = reader.IsDBNull(1) ? "" : reader.GetString(1),
                                    Content = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                    CountComments = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                                });
                            }
                        }
                    }
                    connection.Close();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao Buscar posts via procedure", ex);
            }
            #endregion
        }

        public async Task<int> InsertPostProcedureAsync(string title, string content)
        {
            #region Insert Post
            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    connection.Open();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_CreatePost";
                        command.CommandType = CommandType.StoredProcedure;

                        var titleParam = command.CreateParameter();
                        titleParam.ParameterName = "@Title";
                        titleParam.Value = title;
                        command.Parameters.Add(titleParam);

                        var contentParam = command.CreateParameter();
                        contentParam.ParameterName = "@Content";
                        contentParam.Value = content;
                        command.Parameters.Add(contentParam);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return Convert.ToInt32(reader["Id"]); //retorna o ID criado
                            }
                        }

                    }

                    connection.Close();
                }

                return 0; 
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir post via procedure", ex);
                return 0;
            }
            #endregion
        }

        public async Task<int> CreateCommentToPostProcedureAsync( int postId, string Description )
        {
            #region Insert Post
            try
            {
                using (var connection = _context.Database.GetDbConnection())
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "sp_AddComment";
                        command.CommandType = CommandType.StoredProcedure;

                        var paramPostId = command.CreateParameter();
                        paramPostId.ParameterName = "@PostId";
                        paramPostId.Value = postId;
                        command.Parameters.Add(paramPostId);

                        var paramDesc = command.CreateParameter();
                        paramDesc.ParameterName = "@Description";
                        paramDesc.Value = Description;
                        command.Parameters.Add(paramDesc);

                        using var reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            return Convert.ToInt32(reader["Id"]);
                        }
                    }
                     connection.Close();
                }


                return 0; 
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir post via procedure", ex);
                return 0;
            }
            #endregion
        }
        public async Task<PostComments> FindIdPostProcedureAsync(int Id)
        {
            #region Find Post to Id

            var listaBrutaDoBanco = new List<dynamic>();

            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "sp_GetPostWithComments";
                    command.CommandType = CommandType.StoredProcedure;

                    var param = command.CreateParameter();
                    param.ParameterName = "@PostId";
                    param.Value = Id;
                    command.Parameters.Add(param);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listaBrutaDoBanco.Add(new
                            {
                                PostId = reader.GetInt32(0),
                                Title = reader.GetString(1),
                                Content = reader.GetString(2),
                                CommentId = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                                CommentDescription = reader.IsDBNull(4) ? null : reader.GetString(4),
                            });
                        }
                    }
                }
                connection.Close();
            }

            //Agrupa os Comentarios e monsta um obejto Post com a lista de comentarios
            var postGroup = listaBrutaDoBanco.FirstOrDefault();
            if (postGroup == null) return null;

            return new PostComments
            {
                Id = postGroup.PostId,
                Title = postGroup.Title,
                Content = postGroup.Content,
                Comments = listaBrutaDoBanco
                    .Where(c => c.CommentId != null)
                    .Select(c => new CommentToPost
                    {
                        Id = c.CommentId,
                        Description = c.CommentDescription,
                    })
                    .ToList()
            };
            #endregion
        }
        
    }
}
