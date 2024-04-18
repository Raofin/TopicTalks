namespace TopicTalks.Domain.Interfaces.Core;

public interface IEmailSender
{
    void SendWelcome(string email);
    void SendOtp(string email, string code);
    void SendVerified(string emailAddress);
}