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
        Task<ICollection<ItemColorDto>> GetAllAsync(int page, int take);
        Task<ICollection<ItemColorDto>> GetAllByOrderAsync(int page, int take, Expression<Func<Color, object>>? orderExpression);
        //Task<GetCategoryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateColorDto categoryDto);
        Task UpdateAsync(int id,UpdateColorDto updateCategoryDto);
        Task DeleteAsync(int id);
    }
}
