using Microsoft.AspNetCore.Mvc;
using ServiceA.ContentApI.DTOs.Requests;
using ServiceA.ContentApI.Services;


namespace ServiceA.ContentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _emailService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _emailService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmail(CreateEmailRequestDto request)
        {
            var result = await _emailService.CreateAsync(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateEmailRequestDto request)
        {
            await _emailService.UpdateAsync(id, request);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _emailService.DeleteAsync(id);
            return NoContent();
        }


        [HttpPost("generate")]
        public async Task<IActionResult> Generate(string conetnt)
        {
            var result = await _emailService.SendToServiceBAsync(conetnt);
            return Ok(result);
        }
    }
}
