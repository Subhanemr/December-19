using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos;
using ProniaOnion.Application.Dtos.Categories;
using ProniaOnion.Domain.Entities;
using System.Linq.Expressions;

namespace ProniaOnion.Persistence.Implementations.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateCategoryDto createCategoryDto)
        {
            bool result = await _repository.CheckUnique(c => c.Name == createCategoryDto.name);
            if (result) throw new Exception("Bad Request");
            await _repository.AddAsync(_mapper.Map<Category>(createCategoryDto));
            await _repository.SaveChanceAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Category category = await _repository.GetByIdAsync(id);

            if (category == null) throw new Exception("Not Found");

            _repository.Delete(category);
            await _repository.SaveChanceAsync();
        }

        public async Task<ICollection<ItemCategoryDto>> GetAllAsync(int page, int take)
        {
            ICollection<Category> categories = await _repository.GetAllAsync(skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<ItemCategoryDto> categoryDtos = _mapper.Map<ICollection<ItemCategoryDto>>(categories);

            return categoryDtos;
        }
        public async Task<ICollection<ItemCategoryDto>> GetAllByOrderAsync(int page, int take, Expression<Func<Category, object>>? orderExpression)
        {
            ICollection<Category> categories = await _repository.GetAllByOrderAsync(orderException: orderExpression, skip: (page - 1) * take, take: take, IsTracking: false).ToListAsync();

            ICollection<ItemCategoryDto> categoryDtos = _mapper.Map<ICollection<ItemCategoryDto>>(categories);

            return categoryDtos;
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

        public async Task UpdateAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Category category = await _repository.GetByIdAsync(id);

            if (category == null) throw new Exception("Not Found");
            
            bool result = await _repository.CheckUnique(c => c.Name == updateCategoryDto.name && c.Id != id);
            if (result) throw new Exception("Bad Request");

            _mapper.Map(updateCategoryDto, category);

            _repository.Update(category);
            await _repository.SaveChanceAsync();
        }
    }
}
