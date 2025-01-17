using Vitec.TicketValidation.ExternalContract.Responses;

namespace Vitec.TicketValidation.ApiExample.Host.Services;

public class TestTicketService
{
    private readonly Dictionary<string, TestTicket> _tickets = new()
    {
        // Valid tickets
        {
            "0000AAAA", new TestTicket
            {
                TicketId = "0000AAAA",
                ValidReaderIds = new[] { 1, 2 },
                Status = ValidationStatus.Valid,
                ValidUntil = DateTime.UtcNow.AddDays(30),
                TicketHolder = new TicketHolder { Firstname = "John", Lastname = "Doe" }
            }
        },
        {
            "0000AAAB", new TestTicket
            {
                TicketId = "0000AAAB",
                ValidReaderIds = new[] { 1 },
                Status = ValidationStatus.Valid,
                ValidUntil = DateTime.UtcNow.AddDays(60),
                TicketHolder = new TicketHolder { Firstname = "Jane", Lastname = "Smith" }
            }
        },
        {
            "0000AAAC", new TestTicket
            {
                TicketId = "0000AAAC",
                ValidReaderIds = new[] { 1, 2, 3 },
                Status = ValidationStatus.Valid,
                ValidUntil = null,  // Unlimited validity
                TicketHolder = new TicketHolder { Firstname = "Bob", Lastname = "Johnson" }
            }
        },
        {
            "0000AAAD", new TestTicket
            {
                TicketId = "0000AAAD",
                ValidReaderIds = new[] { 2 },
                Status = ValidationStatus.Valid,
                ValidUntil = DateTime.UtcNow.AddDays(15),
                TicketHolder = null  // Anonymous ticket
            }
        },
        // Expired tickets
        {
            "0000BBBA", new TestTicket
            {
                TicketId = "0000BBBA",
                ValidReaderIds = new[] { 1 },
                Status = ValidationStatus.Expired,
                ValidUntil = DateTime.UtcNow.AddDays(-1),
                TicketHolder = new TicketHolder { Firstname = "Alice", Lastname = "Brown" }
            }
        },
        {
            "0000BBBB", new TestTicket
            {
                TicketId = "0000BBBB",
                ValidReaderIds = new[] { 1, 2 },
                Status = ValidationStatus.Expired,
                ValidUntil = DateTime.UtcNow.AddDays(-30),
                TicketHolder = null
            }
        },
        {
            "0000BBBC", new TestTicket
            {
                TicketId = "0000BBBC",
                ValidReaderIds = new[] { 3 },
                Status = ValidationStatus.Expired,
                ValidUntil = DateTime.UtcNow.AddDays(-5),
                TicketHolder = new TicketHolder { Firstname = "Charlie", Lastname = "Wilson" }
            }
        },
        // Blocked tickets
        {
            "0000CCCA", new TestTicket
            {
                TicketId = "0000CCCA",
                ValidReaderIds = new[] { 1, 2, 3 },
                Status = ValidationStatus.Blocked,
                ValidUntil = DateTime.UtcNow.AddDays(45),
                TicketHolder = new TicketHolder { Firstname = "David", Lastname = "Lee" }
            }
        },
        {
            "0000CCCB", new TestTicket
            {
                TicketId = "0000CCCB",
                ValidReaderIds = new[] { 2 },
                Status = ValidationStatus.Blocked,
                ValidUntil = null,
                TicketHolder = null
            }
        },
        {
            "0000CCCC", new TestTicket
            {
                TicketId = "0000CCCC",
                ValidReaderIds = new[] { 1 },
                Status = ValidationStatus.Blocked,
                ValidUntil = DateTime.UtcNow.AddDays(10),
                TicketHolder = new TicketHolder { Firstname = "Emma", Lastname = "Davis" }
            }
        },
        // Unknown tickets
        {
            "0000DDDA", new TestTicket
            {
                TicketId = "0000DDDA",
                ValidReaderIds = new[] { 1, 2 },
                Status = ValidationStatus.Unknown,
                ValidUntil = null,
                TicketHolder = null
            }
        },
        {
            "0000DDDB", new TestTicket
            {
                TicketId = "0000DDDB",
                ValidReaderIds = new[] { 3 },
                Status = ValidationStatus.Unknown,
                ValidUntil = DateTime.UtcNow.AddDays(20),
                TicketHolder = new TicketHolder { Firstname = "Frank", Lastname = "Miller" }
            }
        },
        // Error tickets
        {
            "0000EEEA", new TestTicket
            {
                TicketId = "0000EEEA",
                ValidReaderIds = new[] { 1 },
                Status = ValidationStatus.Error,
                ValidUntil = null,
                TicketHolder = null
            }
        },
        {
            "0000EEEB", new TestTicket
            {
                TicketId = "0000EEEB",
                ValidReaderIds = new[] { 2, 3 },
                Status = ValidationStatus.Error,
                ValidUntil = DateTime.UtcNow.AddDays(5),
                TicketHolder = new TicketHolder { Firstname = "Grace", Lastname = "Taylor" }
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
                ErrorMessage = "Ticket not found"
            };
        }

        if (!ticket.ValidReaderIds.Contains(readerId))
        {
            return new ValidateTicketResponse
            {
                Status = ValidationStatus.InvalidReader,
                ErrorMessage = "Reader not authorized for this ticket"
            };
        }

        return new ValidateTicketResponse
        {
            Status = ticket.Status,
            ErrorMessage = ticket.Status != ValidationStatus.Valid ? "Ticket is not valid" : null,
            ValidUntil = ticket.ValidUntil,
            TicketHolder = ticket.TicketHolder
        };
    }
}


public class TestTicket
{
    public string TicketId { get; set; }
    public int[] ValidReaderIds { get; set; }
    public ValidationStatus Status { get; set; }
    public DateTime? ValidUntil { get; set; }
    public TicketHolder? TicketHolder { get; set; }
}