using ETicaret.API.Persistence.Models;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductWriteRepository _productWriteRepository;
        IProductReadRepository _productReadRepository;

        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }
        [HttpPost("create")] 
        public async Task<IActionResult> Create(VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
            });
            await _productWriteRepository.SaveAysnc();
            return Ok(true);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id,false);
            return Ok(product);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = _productReadRepository.GetAll(false);
            await _productWriteRepository.SaveAysnc();
            return Ok(result);
        }
    }

}
