namespace Vitec.TicketValidation.ApiExample.Host.Auth;

public class TokenRequest
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string GrantType { get; set; } = "client_credentials";
}