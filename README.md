# Ticket Validation API Contract

## Overview
This document describes the contract for implementing a Ticket Validation API. The API allows our systems to validate tickets using a standardized interface. This enables seamless integration between different ticketing systems while maintaining consistent validation behavior.

This repository contains a reference implementation that you can use as a reference or as a starting point for your own implementation. While you don't need to follow the exact implementation details, it demonstrates the expected behavior, validation rules, and authentication handling required by the contract.

## Authentication
The API must support at least one of the following authentication methods:

### API Key Authentication
- Header: `X-API-Key`
- Format: String
- Example: `X-API-Key: api-key-here`

### OAuth2 (Bearer Token)
- Header: `Authorization`
- Format: `Bearer {token}`
- Example: `Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...`

## Endpoints

### Validate Ticket
Validates a ticket against the system.

```
POST /api/tickets/validate
```

#### Request Model (ValidateTicketRequest)

```csharp
public class ValidateTicketRequest
{
    public string TicketId { get; set; }    // Hexadecimal string
    public int ReaderId { get; set; }       // Numeric identifier for the ticket reader
}
```

| Field | Type | Description |
|-------|------|-------------|
| TicketId | string | A hexadecimal string identifying the ticket. Must contain only characters 0-9 and A-F (case-insensitive). |
| ReaderId | integer | Identifier for the reader/terminal attempting to validate the ticket. |

#### Response Model (ValidateTicketResponse)

```csharp
public class ValidateTicketResponse
{
    public ValidationStatus Status { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime? ValidUntil { get; set; }
    public TicketHolder TicketHolder { get; set; }
}
```

| Field | Type | Description | Required |
|-------|------|-------------|----------|
| Status | ValidationStatus | The status of the ticket validation (see ValidationStatus enum) | Yes |
| ErrorMessage | string | Description of the error if the status is not Valid. Should be null for valid tickets. | Yes if not valid |
| ValidUntil | DateTime? | The expiration date/time of the ticket in UTC. | No |
| TicketHolder | TicketHolder | Information about the ticket holder. | No |

#### ValidationStatus Enum

```csharp
public enum ValidationStatus
{
    Valid,
    Expired,
    Blocked,
    InvalidReader,
    Unknown,
    Error
}
```

| Value | Description |
|-------|-------------|
| Valid | The ticket is valid and can be used |
| Expired | The ticket has expired |
| Blocked | The ticket has been blocked/blacklisted |
| InvalidReader | The reader is not authorized to validate this ticket |
| Unknown | The ticket is not recognized by the system |
| Error | An error occurred during validation |

#### TicketHolder Model

```csharp
public class TicketHolder
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
}
```

| Field | Type | Description | Required |
|-------|------|-------------|----------|
| Firstname | string | First name of the ticket holder | Yes (if TicketHolder is present) |
| Lastname | string | Last name of the ticket holder | Yes (if TicketHolder is present) |

## Response Examples

### Valid Ticket
```json
{
    "status": "Valid",
    "errorMessage": null,
    "validUntil": "2024-02-15T23:59:59Z",
    "ticketHolder": {
        "firstname": "John",
        "lastname": "Doe"
    }
}
```

### Expired Ticket
```json
{
    "status": "Expired",
    "errorMessage": "Ticket expired on 2024-01-01T00:00:00Z",
    "validUntil": "2024-01-01T00:00:00Z",
    "ticketHolder": null
}
```

### Invalid Reader
```json
{
    "status": "InvalidReader",
    "errorMessage": "Reader not authorized for this ticket",
    "validUntil": null,
    "ticketHolder": null
}
```

## Implementation Requirements

1. **Authentication**
   - Must implement at least one of the specified authentication methods
   - Must return 401 Unauthorized for invalid credentials

2. **Input Validation**
   - Must validate TicketId is a valid hexadecimal string
   - Must validate ReaderId is a positive integer
   - Must return 400 Bad Request for invalid input

3. **Response Format**
   - Must always return a ValidateTicketResponse object
   - Must set appropriate HTTP status codes:
     - 200 OK: For all validation responses, even if the ticket is invalid
     - 400 Bad Request: For invalid input format
     - 401: For authentication/authorization issues
     - 500 Internal Server Error: For unexpected errors

4. **Date/Time Handling**
   - All DateTime values must be in UTC

## Error Handling
The API should handle errors gracefully and return appropriate error messages:

- For validation errors, use the ErrorMessage field to provide details
- For system errors, set Status to Error and provide a generic error message
- Never expose internal system details in error messages
- Always maintain the contract structure, even in error cases

## Best Practices
1. Implement robust logging for troubleshooting
2. Use HTTPS for all endpoints
3. Keep error messages user-friendly but informative

## Testing
Implementers should test their API against various scenarios:
- All ValidationStatus values
- Tickets with and without expiration dates
- Tickets with and without holder information
- Various authentication scenarios
- Error cases and edge conditions

## Integration Requirements
When implementing this API contract, you must provide us with the following information:

### Endpoint Information
- The complete URL for the ticket validation endpoint

### Authentication Details
Depending on your chosen authentication method, provide:

#### For API Key Authentication
- The API key to be used
- Any additional requirements for the API key (such as IP restrictions, if any)
- Expected lifetime of the API key

#### For OAuth2 Authentication
- Token endpoint URL
- Client credentials (client_id and client_secret)
- Scope requirements (if any)

### Contact Information
- Technical contact for integration support
- Escalation process for critical issues
- Expected response times for support requests