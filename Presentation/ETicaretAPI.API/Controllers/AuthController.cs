using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        ITokenHandler _tokenHandler;

        public AuthController(ITokenHandler tokenHandler)
        {
            _tokenHandler = tokenHandler;
        }

        [HttpGet("createToken")]
        public IActionResult CreateToken()
        {
            Token token = _tokenHandler.CreateAccessToken(10);
            return Ok();
        }
    }
}
