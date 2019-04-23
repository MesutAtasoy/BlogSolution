using AutoMapper;
using Blog.Application.Dtos;
using Blog.Domain.Models;

namespace Blog.Application.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
