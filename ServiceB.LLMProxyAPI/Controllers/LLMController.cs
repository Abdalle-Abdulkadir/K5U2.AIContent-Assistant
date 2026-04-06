using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LLMController : ControllerBase
{
    [HttpPost]
    public IActionResult GenerateEmail()
    {
        return Ok("LLM response");
    }
}

