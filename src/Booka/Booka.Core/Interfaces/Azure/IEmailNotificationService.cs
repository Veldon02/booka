namespace Booka.Core.Interfaces.Azure;

public interface IEmailNotificationService
{
    Task PushUserRegistrationMessage(string recepient, string firstName);
}