using ETicaretAPI.Application.Repositories;
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
        [HttpGet("post")] 
        public async Task Create()
        {
            await _productWriteRepository.AddAsync(new() 
            { CreatedDate = DateTime.UtcNow, Id=Guid.NewGuid(), Name="Elma", Price=10, Stock=1000}
            );
            await _productWriteRepository.SaveAysnc();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productReadRepository.GetByIdAsync(id,false);
            return Ok(product);
        }
    }

}
