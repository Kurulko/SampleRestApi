using Microsoft.AspNetCore.Mvc;

namespace SampleRestApi.Controllers;

public class PingController : ApiController
{
    [HttpGet]
    public IActionResult Ping()
        => Ok("Dogs house service. Version 1.0.1");
}
