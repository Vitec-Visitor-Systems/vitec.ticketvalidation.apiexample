using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Vitec.TicketValidation.ApiExample.Host.Controllers;

[ApiController]
[AllowAnonymous]
public class HealthController : ControllerBase
{

    [Route("health")]
    [HttpGet]
    public ActionResult Health() => Ok("Healthy");
}