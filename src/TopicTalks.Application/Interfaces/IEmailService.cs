namespace TopicTalks.Application.Interfaces;

public interface IEmailService
{
    void SendWelcome(string email);
    void SendOtp(string email, string code);
    void SendVerified(string emailAddress);
}