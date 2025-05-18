namespace Booka.EmailNotificationFunctionApp.Interfaces;

public interface IEmailService
{
    public Task SendEmail(string recipient, string subject, string emailBody);
}