using Vitec.TicketValidation.ExternalContract.Responses;

namespace Vitec.TicketValidation.ApiExample.Host.Services;

public class TestTicketService
{
    private readonly Dictionary<string, TestTicket> _tickets = new()
    {
            // Valid tickets
            {
                "TICKET-001", new TestTicket
                {
                    TicketId = "TICKET-001",
                    ValidReaderIds = new[] { 1, 2 },
                    Status = ValidationStatus.Valid,
                    EndUserMessage = "Welcome! You have 3 clips remaining on your multi-journey ticket.",
                    ValidUntil = DateTime.UtcNow.AddDays(30),
                    TicketHolder = new TicketHolder { Firstname = "John", Lastname = "Doe" }
                }
            },
            {
                "TICKET-002", new TestTicket
                {
                    TicketId = "TICKET-002",
                    ValidReaderIds = new[] { 1 },
                    Status = ValidationStatus.Valid,
                    EndUserMessage = "Welcome! Your season ticket is valid until February 15th, 2024.",
                    ValidUntil = DateTime.UtcNow.AddDays(60),
                    TicketHolder = new TicketHolder { Firstname = "Jane", Lastname = "Smith" }
                }
            },
            {
                "TICKET-003", new TestTicket
                {
                    TicketId = "TICKET-003",
                    ValidReaderIds = new[] { 1, 2, 3 },
                    Status = ValidationStatus.Valid,
                    EndUserMessage = "Welcome! Your lifetime pass is valid for all stations.",
                    ValidUntil = null,
                    TicketHolder = new TicketHolder { Firstname = "Bob", Lastname = "Johnson" }
                }
            },
            {
                "TICKET-004", new TestTicket
                {
                    TicketId = "TICKET-004",
                    ValidReaderIds = new[] { 2 },
                    Status = ValidationStatus.Valid,
                    EndUserMessage = "Welcome! Single journey ticket validated successfully.",
                    ValidUntil = DateTime.UtcNow.AddHours(2),
                    TicketHolder = null
                }
            },
            // Expired tickets
            {
                "TICKET-101", new TestTicket
                {
                    TicketId = "TICKET-101",
                    ValidReaderIds = new[] { 1 },
                    Status = ValidationStatus.Expired,
                    EndUserMessage = "Your season ticket expired yesterday. Please renew your ticket at the ticket office or online.",
                    ValidUntil = DateTime.UtcNow.AddDays(-1),
                    TicketHolder = new TicketHolder { Firstname = "Alice", Lastname = "Brown" }
                }
            },
            {
                "TICKET-102", new TestTicket
                {
                    TicketId = "TICKET-102",
                    ValidReaderIds = new[] { 1, 2 },
                    Status = ValidationStatus.Expired,
                    EndUserMessage = "Your multi-journey ticket has expired. Please purchase a new ticket.",
                    ValidUntil = DateTime.UtcNow.AddDays(-30),
                    TicketHolder = null
                }
            },
            {
                "TICKET-103", new TestTicket
                {
                    TicketId = "TICKET-103",
                    ValidReaderIds = new[] { 3 },
                    Status = ValidationStatus.Expired,
                    EndUserMessage = "Your single journey ticket has expired. Maximum journey time is 2 hours.",
                    ValidUntil = DateTime.UtcNow.AddHours(-1),
                    TicketHolder = new TicketHolder { Firstname = "Charlie", Lastname = "Wilson" }
                }
            },
            // Blocked tickets
            {
                "TICKET-201", new TestTicket
                {
                    TicketId = "TICKET-201",
                    ValidReaderIds = new[] { 1, 2, 3 },
                    Status = ValidationStatus.Blocked,
                    EndUserMessage = "This ticket has been blocked due to reported loss. Please contact customer service.",
                    ValidUntil = DateTime.UtcNow.AddDays(45),
                    TicketHolder = new TicketHolder { Firstname = "David", Lastname = "Lee" }
                }
            },
            {
                "TICKET-202", new TestTicket
                {
                    TicketId = "TICKET-202",
                    ValidReaderIds = new[] { 2 },
                    Status = ValidationStatus.Blocked,
                    EndUserMessage = "This ticket has been blocked. Please contact customer service for more information.",
                    ValidUntil = null,
                    TicketHolder = null
                }
            },
            {
                "TICKET-203", new TestTicket
                {
                    TicketId = "TICKET-203",
                    ValidReaderIds = new[] { 1 },
                    Status = ValidationStatus.Blocked,
                    EndUserMessage = "This ticket has been blocked due to suspicious activity. Please visit the ticket office.",
                    ValidUntil = DateTime.UtcNow.AddDays(10),
                    TicketHolder = new TicketHolder { Firstname = "Emma", Lastname = "Davis" }
                }
            },
            // Unknown tickets
            {
                "TICKET-301", new TestTicket
                {
                    TicketId = "TICKET-301",
                    ValidReaderIds = new[] { 1, 2 },
                    Status = ValidationStatus.Unknown,
                    EndUserMessage = "This ticket number is not recognized. Please check the ticket number or contact customer service.",
                    ValidUntil = null,
                    TicketHolder = null
                }
            },
            {
                "TICKET-302", new TestTicket
                {
                    TicketId = "TICKET-302",
                    ValidReaderIds = new[] { 3 },
                    Status = ValidationStatus.Unknown,
                    EndUserMessage = "Unable to validate ticket. Please try again or seek assistance from station staff.",
                    ValidUntil = null,
                    TicketHolder = new TicketHolder { Firstname = "Frank", Lastname = "Miller" }
                }
            },
            // Error tickets
            {
                "TICKET-401", new TestTicket
                {
                    TicketId = "TICKET-401",
                    ValidReaderIds = new[] { 1 },
                    Status = ValidationStatus.Error,
                    EndUserMessage = "A system error occurred while validating your ticket. Please try again.",
                    ValidUntil = null,
                    TicketHolder = null
                }
            },
            {
                "TICKET-402", new TestTicket
                {
                    TicketId = "TICKET-402",
                    ValidReaderIds = new[] { 2, 3 },
                    Status = ValidationStatus.Error,
                    EndUserMessage = "Unable to process ticket validation at this time. Please try again later.",
                    ValidUntil = DateTime.UtcNow.AddDays(5),
                    TicketHolder = new TicketHolder { Firstname = "Grace", Lastname = "Taylor" }
                }
            },
            {
                "TICKET-501", new TestTicket
                {
                    TicketId = "TICKET-501",
                    ValidReaderIds = new[] { 1, 2, 3 },
                    Status = ValidationStatus.Valid,
                    EndUserMessage = "Welcome! You have 1 clip remaining on your ticket.",
                    ValidUntil = DateTime.UtcNow.AddDays(15),
                    TicketHolder = new TicketHolder { Firstname = "Henry", Lastname = "Wilson" }
                }
            },
            {
                "TICKET-502", new TestTicket
                {
                    TicketId = "TICKET-502",
                    ValidReaderIds = new[] { 1 },
                    Status = ValidationStatus.Valid,
                    EndUserMessage = "Welcome! Your group ticket is valid for 5 people.",
                    ValidUntil = DateTime.UtcNow.AddHours(4),
                    TicketHolder = new TicketHolder { Firstname = "Isabella", Lastname = "Moore" }
                }
            }
        };

    public ValidateTicketResponse ValidateTicket(string ticketId, int readerId)
    {
        if (!_tickets.TryGetValue(ticketId, out var ticket))
        {
            return new ValidateTicketResponse
            {
                Status = ValidationStatus.Unknown,
                EndUserMessage = "Ticket not found. Please check your ticket number."
            };
        }

        if (!ticket.ValidReaderIds.Contains(readerId))
        {
            return new ValidateTicketResponse
            {
                Status = ValidationStatus.InvalidReader,
                EndUserMessage = "This ticket cannot be used at this location."
            };
        }

        return new ValidateTicketResponse
        {
            Status = ticket.Status,
            EndUserMessage = ticket.EndUserMessage,
            ValidUntil = ticket.ValidUntil,
            TicketHolder = ticket.TicketHolder
        };
    }
}


public class TestTicket
{
    public string TicketId { get; set; }
    public int[] ValidReaderIds { get; set; }
    public string EndUserMessage { get; set; }
    public ValidationStatus Status { get; set; }
    public DateTime? ValidUntil { get; set; }
    public TicketHolder? TicketHolder { get; set; }
}