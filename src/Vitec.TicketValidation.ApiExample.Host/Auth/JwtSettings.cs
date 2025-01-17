namespace Vitec.TicketValidation.ApiExample.Host.Auth;

public class JwtSettings
{
    public const string Issuer = "test-ticket-api";
    public const string Audience = "test-clients";
    // WARNING: This is for testing only! Never use hardcoded keys in production
    public const string SecretKey = "YourSuperSecretKeyForTestingOnly-MakeItLongEnough";
}