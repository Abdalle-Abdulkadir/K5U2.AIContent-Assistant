using Microsoft.AspNetCore.Mvc;
using ServiceB.LLMProxyAPI.DTOs;
using ServiceB.LLMProxyAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class LLMController : ControllerBase
{
    private readonly LlmService _llmService;

    public LLMController(LlmService llmService)
    {
        _llmService = llmService;
    }

    [HttpPost]
    public async Task<IActionResult> GenerateEmail([FromBody] GenerateRequestDto dto)
    {
        var result = await _llmService.GenerateAsync(dto.content);
        return Ok(result);
    }
}

  
