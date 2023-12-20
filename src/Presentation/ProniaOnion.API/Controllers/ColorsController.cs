using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Categories;
using ProniaOnion.Application.Dtos;
using ProniaOnion.Application.Dtos.Color;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorsController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page, int take)
        {
            return Ok(await _colorService.GetAllAsync(page, take));
        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> Get(int id)
        //{
        //    if (id <= 0) return BadRequest();
        //    return Ok(await _categoryService.GetByIdAsync(id));
        //}
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateColorDto createColorDto)
        {
            await _colorService.CreateAsync(createColorDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateColorDto updateColorDto)
        {
            await _colorService.UpdateAsync(id, updateColorDto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _colorService.DeleteAsync(id);
            return NoContent();
        }
    }
}
