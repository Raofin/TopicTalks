namespace TopicTalks.Application.Interfaces;

public interface IEmailService
{
    Task SendWelcome(string email);
}