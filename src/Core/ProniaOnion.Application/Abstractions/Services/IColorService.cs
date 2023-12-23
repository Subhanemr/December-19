using ProniaOnion.Application.Dtos.Categories;
using ProniaOnion.Application.Dtos;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ProniaOnion.Application.Dtos.Color;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface IColorService
    {
        Task<ICollection<ItemColorDto>> GetAllWhere(int page, int take, bool isDeleted = false);
        Task<ICollection<ItemColorDto>> GetAllWhereByOrder(int page, int take, Expression<Func<Color, object>>? orderExpression, bool isDeleted = false);
        //Task<GetCategoryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateColorDto createColorDto);
        Task UpdateAsync(int id,UpdateColorDto updateColorDto);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);

    }
}
