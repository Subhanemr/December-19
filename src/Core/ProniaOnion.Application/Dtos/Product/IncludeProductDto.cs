namespace ProniaOnion.Application.Dtos.Product
{
    public record IncludeProductDto(int id, string name, decimal Price, string SKU, string? Description);
}
