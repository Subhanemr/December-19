using ProniaOnion.Application.Dtos.Tag;
using ProniaOnion.Domain.Entities;
using System.Linq.Expressions;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface ITagService
    {
        Task<ICollection<ItemTagDto>> GetAllWhereAsync(int page, int take, bool isDeleted = false);
        Task<ICollection<ItemTagDto>> GetAllWhereByOrderAsync(int page, int take, Expression<Func<Tag, object>>? orderExpression, bool isDeleted = false);
        Task<GetTagDto> GetByIdAsync(int id);
        Task CreateAsync(CreateTagDto create);
        Task UpdateAsync(int id, UpdateTagDto update);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);

    }
}
