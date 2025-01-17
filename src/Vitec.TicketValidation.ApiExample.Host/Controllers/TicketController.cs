using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitec.TicketValidation.ApiExample.Host.Services;
using Vitec.TicketValidation.ExternalContract.Requests;
using Vitec.TicketValidation.ExternalContract.Responses;

namespace Vitec.TicketValidation.ApiExample.Host.Controllers;

[ApiController]
[Route("api/tickets")]
[Authorize]
public class TicketController(TestTicketService ticketService) : ControllerBase
{
    [HttpPost("validate")]
    public ActionResult<ValidateTicketResponse> ValidateTicket(ValidateTicketRequest request)
    {
        if (!IsValidHexString(request.TicketId))
        {
            return BadRequest(new ValidateTicketResponse
            {
                Status = ValidationStatus.Error,
                ErrorMessage = "Invalid ticket ID format"
            });
        }

        var response = ticketService.ValidateTicket(request.TicketId, request.ReaderId);
        return Ok(response);
    }

    private bool IsValidHexString(string input)
    {
        return !string.IsNullOrEmpty(input) &&
               input.All(c => "0123456789ABCDEFabcdef".Contains(c));
    }
}