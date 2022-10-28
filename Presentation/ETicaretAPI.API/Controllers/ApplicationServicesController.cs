using ETicaretAPI.Application.Abstractions.Services.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationServicesController : ControllerBase
    {
        IAplicationService _aplicationService;

        public ApplicationServicesController(IAplicationService aplicationService)
        {
            _aplicationService = aplicationService;
        }

        [HttpGet]
        public IActionResult GetAuthorizeDefinitionEndPoints()
        {
           var datas= _aplicationService.GetAuthorizeDefinitionEndpoints(typeof(Program));
            return Ok(datas);
        }
    }
}
