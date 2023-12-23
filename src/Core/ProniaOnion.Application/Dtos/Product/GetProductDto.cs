using ProniaOnion.Application.Dtos.Categories;

namespace ProniaOnion.Application.Dtos.Product
{
    public record GetProductDto(int id, string name, decimal Price, string SKU, string? Description, int CategoryId, IncludeCategoryDto Category);
}
