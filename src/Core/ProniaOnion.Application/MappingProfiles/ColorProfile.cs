using AutoMapper;
using ProniaOnion.Application.Dtos.Categories;
using ProniaOnion.Application.Dtos;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.Dtos.Color;

namespace ProniaOnion.Application.MappingProfiles
{
    internal class ColorProfile : Profile
    {
        public ColorProfile()
        {
            CreateMap<CreateColorDto, Color>();
            CreateMap<Color, ItemColorDto>().ReverseMap();
            CreateMap<UpdateColorDto, Color>().ReverseMap();
        }
    }
}
