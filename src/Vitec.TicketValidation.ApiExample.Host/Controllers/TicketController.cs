using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vitec.TicketValidation.ApiExample.Host.Services;
using Vitec.TicketValidation.ExternalContract.Requests;
using Vitec.TicketValidation.ExternalContract.Responses;

namespace Vitec.TicketValidation.ApiExample.Host.Controllers;

[ApiController]
[Route("api/tickets")] // Note: This is just for our reference implementation
[Authorize]
public class TicketController(TestTicketService ticketService, ILogger<TicketController> logger) : ControllerBase
{
    [HttpPost("validate")]
    public ActionResult<ValidateTicketResponse> ValidateTicket(ValidateTicketRequest request)
    {
        logger.LogInformation("Got request to validate for {ticket} and reader {reader}", request.TicketId, request.ReaderId);
        if (string.IsNullOrEmpty(request.TicketId))
        {
            logger.LogInformation("Returning bad request duo to empty ticket Id");
            return BadRequest(new ValidateTicketResponse
            {
                Status = ValidationStatus.Error,
                EndUserMessage = "Ticket ID is required"
            });
        }

        var response = ticketService.ValidateTicket(request.TicketId, request.ReaderId);
        logger.LogInformation("Returning validate response with statues {s} and msg {m}", response.Status.ToString(), response.EndUserMessage);
        return Ok(response);
    }
}