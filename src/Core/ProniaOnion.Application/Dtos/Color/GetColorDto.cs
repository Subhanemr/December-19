using ProniaOnion.Application.Dtos.Product;

namespace ProniaOnion.Application.Dtos.Color
{
    public record GetColorDto(int id, string name, ICollection<IncludeProductDto> products);
}
