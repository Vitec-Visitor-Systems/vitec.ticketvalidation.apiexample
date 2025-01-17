namespace Vitec.TicketValidation.ExternalContract.Responses;

public enum ValidationStatus
{
    Valid,
    Expired,
    Blocked,
    InvalidReader,
    Unknown,
    Error
}