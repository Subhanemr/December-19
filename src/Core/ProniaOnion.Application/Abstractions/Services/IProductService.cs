using ProniaOnion.Application.Dtos.Color;
using ProniaOnion.Application.Dtos.Product;
using ProniaOnion.Domain.Entities;
using System.Linq.Expressions;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<ICollection<ItemProductDto>> GetAllWhereAsync(int page, int take, bool isDeleted = false);
        Task<ICollection<ItemProductDto>> GetAllWhereByOrderAsync(int page, int take, Expression<Func<Product, object>>? orderExpression, bool isDeleted = false);
        //Task<GetCategoryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateProductDto create);
        Task UpdateAsync(int id, UpdateProductDto update);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);
        Task<GetProductDto> GetByIdAsync(int id);
    }
}
