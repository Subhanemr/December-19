using ProniaOnion.Application.Dtos.Product;

namespace ProniaOnion.Application.Dtos.Tag
{
    public record GetTagDto(int id, string name, ICollection<IncludeProductDto> products);
}
