using Booka.Core.Domain.enums.Email;

namespace Booka.Core.DTOs.Azure;

public class EmailQueueMessage
{
    public string Recipient { get; set; }

    public EmailType EmailType { get; set; }

    public object Model { get; set; }
}