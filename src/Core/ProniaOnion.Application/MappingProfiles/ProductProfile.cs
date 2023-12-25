using AutoMapper;
using ProniaOnion.Application.Dtos.Product;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.MappingProfiles
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<Product, ItemProductDto>().ReverseMap();
            CreateMap<GetProductDto, Product>().ReverseMap().ForMember(x => x.Colors, opt => opt.Ignore()).ForMember(x => x.Tags, opt => opt.Ignore());
            CreateMap<UpdateProductDto, Product>().ReverseMap();
            CreateMap<Product, IncludeProductDto>().ReverseMap();

        }
    }
}
