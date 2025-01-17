namespace Vitec.TicketValidation.ExternalContract.Responses;

public class ValidateTicketResponse
{
    public ValidationStatus Status { get; set; }
    public string EndUserMessage { get; set; }
    public DateTime? ValidUntil { get; set; }
    public TicketHolder TicketHolder { get; set; }
}