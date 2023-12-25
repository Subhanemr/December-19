using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Repositories;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Product;
using ProniaOnion.Domain.Entities;
using System.Drawing;
using System.Linq.Expressions;

namespace ProniaOnion.Persistence.Implementations.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IColorRepository _colorRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper, ICategoryRepository categoryRepository, IColorRepository colorRepository, ITagRepository tagRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _colorRepository = colorRepository;
            _tagRepository = tagRepository;
        }

        public async Task CreateAsync(CreateProductDto create)
        {
            bool result = await _repository.CheckUniqueAsync(c => c.Name == create.name);
            if (result) throw new Exception("Name was used");

            bool categoryResult = await _categoryRepository.CheckUniqueAsync(c => c.Id == create.categoryId);
            if (!categoryResult) throw new Exception("Category not exsist");

            Product item = _mapper.Map<Product>(create);
            item.ProductColors = new List<ProductColor>();
            foreach (var colorId in create.colorIds)
            {
                bool colorResult = await _colorRepository.CheckUniqueAsync(x => x.Id == colorId);
                if (!colorResult) throw new Exception("Color not exsist");
                item.ProductColors.Add(new ProductColor { ColorId = colorId });
            }
            item.ProductTags = new List<ProductTag>();
            foreach (var tagId in create.tagIds)
            {
                bool tagResult = await _tagRepository.CheckUniqueAsync(x => x.Id == tagId);
                if (!tagResult) throw new Exception("Color not exsist");
                item.ProductTags.Add(new ProductTag { TagId = tagId });
            }

            await _repository.AddAsync(item);
            await _repository.SaveChanceAsync();
        }

        public async Task DeleteAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            string[] includes = { $"{nameof(Product.ProductColors)}", $"{nameof(Product.ProductTags)}" };

            Product item = await _repository.GetByIdAsync(id, IsDeleted: true, includes: includes);

            if (item == null) throw new Exception("Not Found");

            _repository.Delete(item);
            await _repository.SaveChanceAsync();
        }

        public async Task<ICollection<ItemProductDto>> GetAllWhereAsync(int page, int take, bool isDeleted = false)
        {
            ICollection<Product> items = await _repository
                .GetAllWhere(skip: (page - 1) * take, take: take, IsDeleted: isDeleted, IsTracking: false).ToListAsync();

            ICollection<ItemProductDto> dtos = _mapper.Map<ICollection<ItemProductDto>>(items);

            return dtos;
        }
        public async Task<ICollection<ItemProductDto>> GetAllWhereByOrderAsync(int page, int take, 
            Expression<Func<Product, object>>? orderExpression, bool isDeleted = false)
        {
            ICollection<Product> items = await _repository
                .GetAllWhereByOrder(orderException: orderExpression, skip: (page - 1) * take, take: take, IsDeleted: isDeleted, IsTracking: false).ToListAsync();

            ICollection<ItemProductDto> dtos = _mapper.Map<ICollection<ItemProductDto>>(items);

            return dtos;
        }

        public async Task SoftDeleteAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            string[] includes = { $"{nameof(Product.ProductColors)}", $"{nameof(Product.ProductTags)}" };
            Product item = await _repository.GetByIdAsync(id, includes: includes);
            if (item == null) throw new Exception("Not Found");
            
            _repository.SoftDelete(item);

            foreach (ProductColor productColor in item.ProductColors)
            {
                productColor.IsDeleted = true;
            }

            foreach (ProductTag productTag in item.ProductTags)
            {
                productTag.IsDeleted = true;
            }

            await _repository.SaveChanceAsync();
        }
        public async Task ReverseSoftDeleteAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            Product item = await _repository.GetByIdAsync(id);
            if (item == null) throw new Exception("Not Found");

            _repository.ReverseSoftDelete(item);


            foreach (ProductColor productColor in item.ProductColors)
            {
                productColor.IsDeleted = true;
            }

            foreach (ProductTag productTag in item.ProductTags)
            {
                productTag.IsDeleted = true;
            }

            await _repository.SaveChanceAsync();
        }

        public async Task UpdateAsync(int id, UpdateProductDto update)
        {
            if (id <= 0) throw new Exception("Bad Request");
            string[] includes = { $"{nameof(Product.ProductColors)}", $"{nameof(Product.ProductTags)}" };
            Product item = await _repository.GetByIdAsync(id, includes: includes);

            if (item == null) throw new Exception("Not Found");

            bool result = await _repository.CheckUniqueAsync(c => c.Name == update.name && c.Id != id);
            if (result) throw new Exception("Name was used");

            bool categoryResult = await _categoryRepository.CheckUniqueAsync(c => c.Id == update.categoryId);
            if (update.categoryId != item.CategoryId)
                if (!categoryResult) throw new Exception("Category not exsist");

            item =  _mapper.Map(update, item);
            item.ProductColors = item.ProductColors.Where(pc => update.colorIds.Any(colId => pc.ColorId == colId)).ToList();
            item.ProductTags = item.ProductTags.Where(pc => update.tagIds.Any(tagId => pc.TagId == tagId)).ToList();

            foreach (var colorId in update.colorIds)
            {
                bool colorResult = await _colorRepository.CheckUniqueAsync(x => x.Id == colorId);
                if (!colorResult) throw new Exception("Color not exsist");

                if (!item.ProductColors.Any(pc => pc.ColorId == colorId))
                {
                    item.ProductColors.Add(new ProductColor { ColorId = colorId });
                }
            }
            foreach (var tagId in update.tagIds)
            {
                bool colorResult = await _tagRepository.CheckUniqueAsync(x => x.Id == tagId);
                if (!colorResult) throw new Exception("Color not exsist");

                if (!item.ProductTags.Any(pc => pc.TagId == tagId))
                {
                    item.ProductTags.Add(new ProductTag { TagId = tagId });
                }
            }

            _repository.Update(item);
            await _repository.SaveChanceAsync();
        }

        public async Task<GetProductDto> GetByIdAsync(int id)
        {
            if (id <= 0) throw new Exception("Bad Request");
            string[] includes = { $"{nameof(Product.ProductColors)}", $"{nameof(Product.ProductTags)}", };
            Product item = await _repository.GetByIdAsync(id, includes: nameof(Product.Category));
            if (item == null) throw new Exception("Not Found");

            GetProductDto dto = _mapper.Map<GetProductDto>(item);

            return dto;
        }
    }
}
