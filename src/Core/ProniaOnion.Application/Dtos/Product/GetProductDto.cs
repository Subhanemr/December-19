using ProniaOnion.Application.Dtos.Categories;
using ProniaOnion.Application.Dtos.Color;
using ProniaOnion.Application.Dtos.Tag;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.Dtos.Product
{
    public record GetProductDto()
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public string SKU { get; init; }
        public string Description { get; init; }
        public int CategoryId { get; init; }
        public IncludeCategoryDto Category { get; init; }
        public ICollection<IncludeColorDto> Colors { get; set; }
        public ICollection<IncludeTagDto> Tags { get; set;}

    }
}
