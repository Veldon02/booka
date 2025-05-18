using Azure.Messaging.ServiceBus;
using Booka.EmailNotificationFunctionApp.Interfaces;
using Booka.EmailNotificationFunctionApp.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RazorLight;
using EmailMessage = Booka.EmailNotificationFunctionApp.Models.EmailMessage;

namespace Booka.EmailNotificationFunctionApp.Functions;

public class EmailNotificationFunction
{
    private readonly ILogger<EmailNotificationFunction> _logger;
    private readonly IEmailService _emailService;

    public EmailNotificationFunction(ILogger<EmailNotificationFunction> logger, IEmailService emailService)
    {
        _logger = logger;
        _emailService = emailService;
    }

    [Function(nameof(EmailNotificationFunction))]
    public async Task Run(
        [ServiceBusTrigger("%ServiceBus:QueueName%", Connection = "ServiceBus:ConnectionString")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        var messageBody = message.Body.ToString();

        var emailMessage = JsonConvert.DeserializeObject<EmailMessage>(messageBody);

        try
        {
            await SendEmail(emailMessage);

            await messageActions.CompleteMessageAsync(message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing email message: {ExceptionMessage}", ex.Message);
            
            await messageActions.AbandonMessageAsync(message);
        }
    }

    private async Task SendEmail(EmailMessage emailMessage)
    {
        var templateName = GetTemplateNameByEmailType(emailMessage.EmailType);
        var subject = GetSubjectByEmailType(emailMessage.EmailType);

        var templatePath = Path.Combine(AppContext.BaseDirectory, "EmailTemplates");
        var razorEngine = new RazorLightEngineBuilder()
            .UseFileSystemProject(templatePath)
            .Build();

        var body = await razorEngine.CompileRenderAsync(templateName, emailMessage.Model);

        await _emailService.SendEmail(emailMessage.Recipient, subject, body);
    }

    private static string GetTemplateNameByEmailType(EmailType emailType)
    {
        return emailType switch
        {
            EmailType.UserRegistration => "UserRegistrationEmail.cshtml",
            _ => throw new ArgumentException("Not supported email type")
        };
    }

    private static string GetSubjectByEmailType(EmailType emailType)
    {
        return emailType switch
        {
            EmailType.UserRegistration => "Thanks for your registration!",
            _ => throw new ArgumentException("Not supported email type")
        };
    }
}