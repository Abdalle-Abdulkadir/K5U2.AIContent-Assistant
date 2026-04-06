using Microsoft.AspNetCore.Mvc;

namespace ServiceA.ContentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailsController :ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Service A working");
        }

    }
}
