namespace TopicTalks.Domain.Interfaces.Core;

public interface IEmailSender
{
    void SendWelcome(string emailAddress);
    void SendWelcomeWithOtp(string emailAddress, string code);
    void SendOtp(string emailAddress, string code);
    void SendVerified(string emailAddress);
    void SendAnswerNotification(string emailAddress, string author, string answer, string question);
}