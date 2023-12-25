namespace ProniaOnion.Application.Dtos.Product
{
    public record ItemProductDto(int id, string name, decimal price, string sku, string? description, int categoryId);
}
