using Domain.IRepository;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {

            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var category = await _categoryRepository.Get(id);

            return Ok(new ResponseModel
            {
                Status = category != null ? ApiStatus.Succeeded : ApiStatus.Failed,
                Data = category
            });

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAll();

            return Ok(new ResponseModel
            {
                Status = ApiStatus.Succeeded,
                Data = categories
            });

        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name,
                Description = categoryDto.Description,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow

            };
            var result = await _categoryRepository.Create(category);

            return Ok(new ResponseModel
            {
                Status = result ? ApiStatus.Succeeded : ApiStatus.Failed,
                Data = category
            });

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CategoryDto categoryDto, string id)
        {
            var category = await _categoryRepository.Get(id);

            if (category == null)
                return NotFound();

            category.Name = categoryDto.Name;
            category.Description = categoryDto.Description;
            category.UpdatedDate = DateTime.UtcNow;

            var result = await _categoryRepository.Update(category, id);

            return Ok(new ResponseModel
            {
                Status = result ? ApiStatus.Succeeded : ApiStatus.Failed,
                Data = category
            });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var category = await _categoryRepository.Get(id);

            if (category == null)
                return NotFound();

            var result = await _categoryRepository.Delete(id);

            return Ok(new ResponseModel
            {
                Status = result ? ApiStatus.Succeeded : ApiStatus.Failed,
                 
            });

        }
    }
}
