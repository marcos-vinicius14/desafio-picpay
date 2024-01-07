using Microsoft.AspNetCore.Mvc;

namespace Picpay_01.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public IActionResult ShowAmbient(
        [FromServices] IConfiguration configuration)
    {
        var env = configuration.GetValue<string>("env");
        return Ok(new
        {
            Environment = env
        });
    }
}