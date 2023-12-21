using ProniaOnion.Application.Dtos;
using ProniaOnion.Application.Dtos.Categories;
using ProniaOnion.Domain.Entities;
using System.Linq.Expressions;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<ICollection<ItemCategoryDto>> GetAllAsync(int page, int take);
        Task<ICollection<ItemCategoryDto>> GetAllByOrderAsync(int page, int take, Expression<Func<Category, object>>? orderExpression);
        //Task<GetCategoryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateCategoryDto createCategoryDto);
        Task UpdateAsync(int id, UpdateCategoryDto updateCategoryDto);
        Task DeleteAsync(int id);
    }
}
