namespace Booka.EmailNotificationFunctionApp.Models;

public class EmailMessage
{
    public string Recipient { get; set; }

    public EmailType EmailType { get; set; }

    public object Model { get; set; }
}