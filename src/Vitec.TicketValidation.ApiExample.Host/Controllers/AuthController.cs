using Microsoft.AspNetCore.Mvc;
using Vitec.TicketValidation.ApiExample.Host.Auth;

namespace Vitec.TicketValidation.ApiExample.Host.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(TokenService tokenService) : ControllerBase
{
    private readonly Dictionary<string, string> _validClients = new()
    {
        { "test_client", "test_secret" }
    };

    [HttpPost("token")]
    public IActionResult GetToken(TokenRequest request)
    {
        if (!_validClients.TryGetValue(request.ClientId, out var expectedSecret) ||
            request.ClientSecret != expectedSecret)
        {
            return Unauthorized();
        }

        var token = tokenService.GenerateJwtToken(request.ClientId);

        return Ok(new
        {
            access_token = token,
            token_type = "Bearer",
            expires_in = 3600
        });
    }
}