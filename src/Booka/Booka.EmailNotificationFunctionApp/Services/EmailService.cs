using Azure.Communication.Email;
using Azure;
using Booka.EmailNotificationFunctionApp.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Booka.EmailNotificationFunctionApp.Services;

public class EmailService : IEmailService
{
	private readonly IConfiguration _configuration;

	public EmailService(IConfiguration configuration)
	{
		_configuration = configuration;
	}

    public async Task SendEmail(string recipient, string subject, string emailBody)
    {
        var connectionString = _configuration["EmailCommunication:ConnectionString"];
        var domain = _configuration["EmailCommunication:Domain"];

        var emailClient = new EmailClient(connectionString);

        var emailMessage = new EmailMessage(
            senderAddress: $"DoNotReply@{domain}",
            content: new EmailContent(subject)
            {
                Html = emailBody
            },
            recipients: new EmailRecipients([new EmailAddress(recipient)]));

        await emailClient.SendAsync(WaitUntil.Completed, emailMessage);
    }
}