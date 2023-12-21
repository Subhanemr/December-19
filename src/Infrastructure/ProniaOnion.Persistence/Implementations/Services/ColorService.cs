using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Color;
using ProniaOnion.Domain.Entities;
using System.Linq.Expressions;

namespace ProniaOnion.Persistence.Implementations.Services
{
    public class ColorService : IColorService
    {
        private readonly IColorRepository _repository;
        private readonly IMapper _mapper;

        public ColorService(IColorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(CreateColorDto createColorDto)
        {
            bool result = await _repository.CheckUnique(c => c.Name == createColorDto.name);
            if (result) throw new Exception("Bad Request");
            await _repository.AddAsync(_mapper.Map<Color>(createColorDto));
            await _repository.SaveChanceAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Color color = await _repository.GetByIdAsync(id);

            if (color == null) throw new Exception("Not Found");

            _repository.Delete(color);
            await _repository.SaveChanceAsync();
        }

        public async Task<ICollection<ItemColorDto>> GetAllAsync(int page, int take, bool isDeleted = false)
        {
            ICollection<Color> colors = await _repository.GetAllAsync(skip: (page - 1) * take, take: take, IsDeleted: isDeleted, IsTracking: false).ToListAsync();

            ICollection<ItemColorDto> colorDtos = _mapper.Map<ICollection<ItemColorDto>>(colors);

            return colorDtos;
        }
        public async Task<ICollection<ItemColorDto>> GetAllByOrderAsync(int page, int take, Expression<Func<Color, object>>? orderExpression, bool isDeleted = false)
        {
            ICollection<Color> colors = await _repository.GetAllByOrderAsync(orderException: orderExpression, skip: (page - 1) * take, take: take, IsDeleted: isDeleted, IsTracking: false).ToListAsync();

            ICollection<ItemColorDto> colorDtos = _mapper.Map<ICollection<ItemColorDto>>(colors);

            return colorDtos;
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Color color = await _repository.GetByIdAsync(id);
            if (color == null) throw new Exception("Not Found");
            _repository.SoftDelete(color);
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

        public async Task UpdateAsync(int id, UpdateColorDto updateColorDto)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Color color = await _repository.GetByIdAsync(id);

            if (color == null) throw new Exception("Not Found");

            bool result = await _repository.CheckUnique(c => c.Name == updateColorDto.name && c.Id != id);
            if (result) throw new Exception("Bad Request");

            _mapper.Map(updateColorDto, color);

            _repository.Update(color);
            await _repository.SaveChanceAsync();
        }
    }
}
