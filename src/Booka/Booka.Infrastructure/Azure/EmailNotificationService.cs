using System.Text;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Booka.Core.Domain.enums.Email;
using Booka.Core.DTOs.Azure;
using Booka.Core.Interfaces.Azure;
using Microsoft.Extensions.Configuration;

namespace Booka.Infrastructure.Azure;

public class EmailNotificationService : IEmailNotificationService
{
    private readonly ServiceBusClient _serviceBusClient;
    private readonly IConfiguration _configuration;

    public EmailNotificationService(ServiceBusClient client, IConfiguration configuration)
    {
        _serviceBusClient = client;
        _configuration = configuration;
    }

    public async Task PushUserRegistrationMessage(string recepient, string firstName)
    {
        var emailMessage = new EmailQueueMessage
        {
            Recipient = recepient,
            EmailType = EmailType.UserRegistration,
            Model = firstName
        };

        await PushMessage(emailMessage);
    }

    private async Task PushMessage(object messageBody)
    {
        var queueName = _configuration["ServiceBus:QueueName"];

        await using var sender = _serviceBusClient.CreateSender(queueName);

        var message = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(messageBody));

        await sender.SendMessageAsync(new ServiceBusMessage(message));
    }
}