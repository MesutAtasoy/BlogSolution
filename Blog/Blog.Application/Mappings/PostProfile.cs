using AutoMapper;
using Blog.Application.Dtos;
using Blog.Domain.Models;

namespace Blog.Application.Mappings
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>().ReverseMap();
        }
    }
}
