using ProniaOnion.Application.Dtos.Color;
using ProniaOnion.Application.Dtos.Product;
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
        Task<ICollection<ItemTagDto>> GetAllWhere(int page, int take, bool isDeleted = false);
        Task<ICollection<ItemTagDto>> GetAllWhereByOrder(int page, int take, Expression<Func<Tag, object>>? orderExpression, bool isDeleted = false);
        Task CreateAsync(CreateTagDto create);
        Task UpdateAsync(int id, UpdateTagDto update);
        Task DeleteAsync(int id);
        Task SoftDeleteAsync(int id);
        Task ReverseSoftDeleteAsync(int id);

    }
}
