using ETicaretAPI.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpGet]
        public async Task<IActionResult> SendMail([FromQuery] string to, string subject, string body)
        {
            await _mailService.SendMailAsync(to, subject, body);
            return Ok();
        }
    }
}
