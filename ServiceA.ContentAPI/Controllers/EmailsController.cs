using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
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


        /// <summary>
        /// Get all emails (optional filter by subject)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? subject)
        {
            var result = await _emailService.GetAllAsync(subject);
            return Ok(result);
        }


        /// <summary>
        /// Get email by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _emailService.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Create a new email
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateEmail(CreateEmailRequestDto request)
        {
            var result = await _emailService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Update an existing email
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateEmailRequestDto request)
        {
            var updated = await _emailService.UpdateAsync(id, request);

            if (!updated)
                return NotFound();

            return NoContent();
        }


        /// <summary>
        /// Delete an email
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _emailService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }


        /// <summary>
        /// Generate AI email via Service B
        /// </summary>
        [HttpPost("generate")]
        [EnableRateLimiting("ai-limit")]
        public async Task<IActionResult> Generate(CreateEmailRequestDto dto)
        {
            var result = await _emailService.SendToServiceBAsync(dto);
            return Ok(result);
        }
    }
}
