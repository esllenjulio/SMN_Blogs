using AutoMapper;
using SMN_Blog.API.DTO;
using SMN_Blog.Domain.Entities;

namespace SMN_Blog.API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Post, PostResumeDto>()
            .ForMember(dest => dest.CountComments,
                       opt => opt.MapFrom(src => src.Comments.Count));

            CreateMap<PostDto, Post>().ReverseMap();
            CreateMap<PostResumeDto, PostResume>().ReverseMap();

            CreateMap<CreateOrUpdatePostDto, Post>().ReverseMap();

            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Comment, CreateCommentDto>().ReverseMap();
        }
    }
}
