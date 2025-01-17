namespace Vitec.TicketValidation.ExternalContract.Requests;

public class ValidateTicketRequest
{
    public string TicketId { get; set; }
    public int ReaderId { get; set; }
}