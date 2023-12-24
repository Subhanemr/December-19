using AutoMapper;
using ProniaOnion.Application.Dtos.Color;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    internal class ColorProfile : Profile
    {
        public ColorProfile()
        {
            CreateMap<CreateColorDto, Color>();
            CreateMap<Color, ItemColorDto>().ReverseMap();
            CreateMap<UpdateColorDto, Color>().ReverseMap();
            CreateMap<Color, GetColorDto>().ReverseMap();

        }
    }
}
