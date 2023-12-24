using ProniaOnion.Application.Dtos.Product;

namespace ProniaOnion.Application.Dtos.Categories
{
    public record GetCategoryDto(int id, string name, ICollection<IncludeProductDto> products);
}
