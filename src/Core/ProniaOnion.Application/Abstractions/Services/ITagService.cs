using ProniaOnion.Application.Dtos.Color;
using ProniaOnion.Application.Dtos.Tag;
using ProniaOnion.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProniaOnion.Application.Abstractions.Services
{
    public interface ITagService
    {
        Task<ICollection<ItemTagDto>> GetAllAsync(int page, int take);
        Task<ICollection<ItemTagDto>> GetAllByOrderAsync(int page, int take, Expression<Func<Tag, object>>? orderExpression);
        //Task<GetCategoryDto> GetByIdAsync(int id);
        Task CreateAsync(CreateTagDto createTagDto);
        Task UpdateAsync(int id, UpdateTagDto updateTagDto);
        Task DeleteAsync(int id);
    }
}
