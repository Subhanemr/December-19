namespace ProniaOnion.Application.Dtos.Product
{
    public record CreateProductDto(string name, decimal price, string sku, string? description, int categoryId, ICollection<int> colorIds, ICollection<int> tagIds);
}
