using AutoMapper;
using ProniaOnion.Application.Dtos;
using ProniaOnion.Application.Dtos.Categories;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    internal class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, ItemCategoryDto>().ReverseMap();
            CreateMap<UpdateCategoryDto, Category>().ReverseMap();

        }
    }
}
