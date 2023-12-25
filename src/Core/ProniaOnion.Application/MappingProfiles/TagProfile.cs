using AutoMapper;
using ProniaOnion.Application.Dtos.Tag;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    internal class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<CreateTagDto, Tag>();
            CreateMap<Tag, ItemTagDto>().ReverseMap();
            CreateMap<UpdateTagDto, Tag>().ReverseMap();
            CreateMap<Tag, GetTagDto>().ReverseMap();
            CreateMap<Tag, IncludeTagDto>().ReverseMap();

        }
    }
}
