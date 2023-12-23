using ProniaOnion.Application.Dtos;
using ProniaOnion.Application.Dtos.Categories;
using ProniaOnion.Domain.Entities;
using System.Linq.Expressions;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<ICollection<ItemCategoryDto>> GetAllWhere(int page, int take, bool isDeleted = false);
        Task<ICollection<ItemCategoryDto>> GetAllWhereByOrder(int page, int take, Expression<Func<Category, object>>? orderExpression, bool isDeleted = false);
        //Task<GetCategoryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateCategoryDto createCategoryDto);
        Task UpdateAsync(int id, UpdateCategoryDto updateCategoryDto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);

    }
}
