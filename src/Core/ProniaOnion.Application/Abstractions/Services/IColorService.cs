using ProniaOnion.Application.Dtos.Color;
using ProniaOnion.Domain.Entities;
using System.Linq.Expressions;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IColorService
    {
        Task<ICollection<ItemColorDto>> GetAllWhere(int page, int take, bool isDeleted = false);
        Task<ICollection<ItemColorDto>> GetAllWhereByOrder(int page, int take, Expression<Func<Color, object>>? orderExpression, bool isDeleted = false);
        Task<GetColorDto> GetByIdAsync(int id);
        Task CreateAsync(CreateColorDto create);
        Task UpdateAsync(int id,UpdateColorDto update);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);

    }
}
