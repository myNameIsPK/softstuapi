using Microsoft.AspNetCore.Mvc;

namespace SoftStuApi.Controllers
{
    [Route("api/[Controller]")] // api/helloworld
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public IActionResult print()
        {
            return Ok("FuckHello");
        }
    }
}