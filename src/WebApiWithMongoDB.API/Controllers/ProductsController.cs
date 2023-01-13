using Domain.IRepository;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApiWithMongoDB.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {

            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            var product = await _productRepository.Get(id);

            return Ok(new ResponseModel
            {
                Status = product != null ? ApiStatus.Succeeded : ApiStatus.Failed,
                Data = product
            });

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAll();

            return Ok(new ResponseModel
            {
                Status = ApiStatus.Succeeded,
                Data = products
            });

        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                CategoryId = productDto.CategoryId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow

            };
            var result = await _productRepository.Create(product);

            return Ok(new ResponseModel
            {
                Status = result ? ApiStatus.Succeeded : ApiStatus.Failed,
                Data = product
            });

        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductDto productDto, string id)
        {
            var product = await _productRepository.Get(id);

            if (product == null)
                return NotFound();

            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.UpdatedDate = DateTime.UtcNow;
            product.CategoryId = productDto.CategoryId;

            var result = await _productRepository.Update(product, id);

            return Ok(new ResponseModel
            {
                Status = result ? ApiStatus.Succeeded : ApiStatus.Failed,
                Data = product
            });

        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _productRepository.Get(id);

            if (product == null)
                return NotFound();

            var result = await _productRepository.Delete(id);

            return Ok(new ResponseModel
            {
                Status = result ? ApiStatus.Succeeded : ApiStatus.Failed,

            });

        }
    }
}
