using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos;
using ProniaOnion.Application.Dtos.Categories;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet("[Action]")]
        public async Task<IActionResult> Get(int page, int take, bool isDeleted = false)
        {
            return Ok(await _service.GetAllWhereAsync(page, take, isDeleted: isDeleted));
        }
        [HttpGet("[Action]/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpPost("[Action]")]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDto create)
        {
            await _service.CreateAsync(create);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("[Action]/{id}")]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCategoryDto update)
        {
            await _service.UpdateAsync(id, update);
            return NoContent();
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            await _service.SoftDeleteAsync(id);
            return NoContent();
        }
        [HttpDelete("[Action]/{id}")]
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> ReverseSoftDelete(int id)
        {
            await _service.ReverseSoftDeleteAsync(id);
            return NoContent();
        }
    }
}
