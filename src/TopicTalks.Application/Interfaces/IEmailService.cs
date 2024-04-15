namespace TopicTalks.Application.Interfaces;

public interface IEmailService
{
    Task SendWelcomeAsync(string email);
    Task SendOtpAsync(string email, string code);
    Task SendVerifiedAsync(string emailAddress);
}