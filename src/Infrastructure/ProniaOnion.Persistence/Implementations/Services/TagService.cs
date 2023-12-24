using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Tag;
using ProniaOnion.Domain.Entities;
using System.Linq.Expressions;

namespace ProniaOnion.Persistence.Implementations.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;
        private readonly IMapper _mapper;

        public TagService(ITagRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateTagDto create)
        {
            bool result = await _repository.CheckUniqueAsync(c => c.Name == create.name);
            if (result) throw new Exception("Bad Request");
            await _repository.AddAsync(_mapper.Map<Tag>(create));
            await _repository.SaveChanceAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Tag item = await _repository.GetByIdAsync(id);

            if (item == null) throw new Exception("Not Found");

            _repository.Delete(item);
            await _repository.SaveChanceAsync();
        }

        public async Task<ICollection<ItemTagDto>> GetAllWhere(int page, int take, bool isDeleted = false)
        {
            ICollection<Tag> items = await _repository
                .GetAllWhere(skip: (page - 1) * take, take: take, IsDeleted: isDeleted, IsTracking: false).ToListAsync();

            ICollection<ItemTagDto> dtos = _mapper.Map<ICollection<ItemTagDto>>(items);

            return dtos;
        }
        public async Task<ICollection<ItemTagDto>> GetAllWhereByOrder(int page, int take, 
            Expression<Func<Tag, object>>? orderExpression, bool isDeleted = false)
        {
            ICollection<Tag> items = await _repository
                .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsDeleted: isDeleted, IsTracking: false).ToListAsync();

            ICollection<ItemTagDto> dtos = _mapper.Map<ICollection<ItemTagDto>>(items);

            return dtos;
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Tag item = await _repository.GetByIdAsync(id);
            if (item == null) throw new Exception("Not Found");
            _repository.SoftDelete(item);
            await _repository.SaveChanceAsync();
        }

        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Tag item = await _repository.GetByIdAsync(id);
            if (item == null) throw new Exception("Not Found");
            _repository.ReverseSoftDelete(item);
            await _repository.SaveChanceAsync();
        }

        //public async Task<GetCategoryDto> GetByIdAsync(int id)
        //{
        //    Category category = await _repository.GetByIdAsync(id);
        //    if (category == null) throw new Exception("Not Found");

        //    return new GetCategoryDto
        //    {
        //        Id = category.Id,
        //        Name = category.Name
        //    };
        //}

        public async Task UpdateAsync(int id, UpdateTagDto update)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Tag item = await _repository.GetByIdAsync(id);

            if (item == null) throw new Exception("Not Found");

            bool result = await _repository.CheckUniqueAsync(c => c.Name == update.name && c.Id != id);
            if (result) throw new Exception("Bad Request");

            _mapper.Map(update, item);

            _repository.Update(item);
            await _repository.SaveChanceAsync();
        }
    }
}
