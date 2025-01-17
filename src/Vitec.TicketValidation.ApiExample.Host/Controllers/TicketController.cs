using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitec.TicketValidation.ApiExample.Host.Services;
using Vitec.TicketValidation.ExternalContract.Requests;
using Vitec.TicketValidation.ExternalContract.Responses;

namespace Vitec.TicketValidation.ApiExample.Host.Controllers;

[ApiController]
[Route("api/tickets")] // Note: This is just for our reference implementation
[Authorize]
public class TicketController(TestTicketService ticketService) : ControllerBase
{
    [HttpPost("validate")]
    public ActionResult<ValidateTicketResponse> ValidateTicket(ValidateTicketRequest request)
    {
        if (string.IsNullOrEmpty(request.TicketId))
        {
            return BadRequest(new ValidateTicketResponse
            {
                Status = ValidationStatus.Error,
                EndUserMessage = "Ticket ID is required"
            });
        }

        var response = ticketService.ValidateTicket(request.TicketId, request.ReaderId);
        return Ok(response);
    }
}