using FluentEmail.Core;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Infrastructure.Services.Email;

internal class EmailSender(IFluentEmail fluentEmail) : IEmailSender
{
    private readonly IFluentEmail _fluentEmail = fluentEmail;

    public void SendWelcome(string emailAddress)
    {
        var email = _fluentEmail
            .To(emailAddress)
            .Subject("TopicTalks Account")
            .Body("""
                    <body style='font-family: Inter, Arial, sans-serif;'>
                      <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 5px;'>
                        <h2 style='color: #333; text-align: center;'>Registration Successful!</h2>
                        <p>Welcome to TopicTalks! Your registration was successful.</p>
                        <p>
                          Thank you for becoming a part of our community. If you have any questions, feel free to contact our
                          support team at <a href='mailto:support@topictalks.rawfin.net'>support@topictalks.rawfin.net</a>.
                        </p>
                        <p>Best,<br>TopicTalks Team</p>
                      </div>
                    </body>
                  """,
                isHtml: true);

        Send(email);
    }

    public void SendOtp(string emailAddress, string code)
    {
        var email = _fluentEmail
            .To(emailAddress)
            .Subject("TopicTalks Account")
            .Body($"""
                    <body style='font-family: Inter, Arial, sans-serif;'>
                      <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 5px;'>
                        <h2 style='color: #333; text-align: center;'>Email Verification</h2>
                        <p>Thank you for signing up with TopicTalks! Please use the following code to verify your account.</p>
                        <div style='text-align: center; line-height: 25px;'>
                          <div style='margin-bottom: 5px;'>
                              <span>Verification Code</span><br>
                              <span style='font-size: 20px; font-weight: 700;'>{code}</span><br>
                              <span>(This code is valid for 5 minutes)</span>
                          </div>
                        </div>
                        <p>If you did not request this OTP, please ignore this email.</p>
                        <p>Best,<br>TopicTalks Team</p>
                      </div>
                    </body>
                  """,
                isHtml: true);

        Send(email);
    }

    public void SendVerified(string emailAddress)
    {
        var email = _fluentEmail
            .To(emailAddress)
            .Subject("TopicTalks Account")
            .Body("""
                    <body style='font-family: Inter, Arial, sans-serif;'>
                      <div style='max-width: 600px; margin: 0 auto; padding: 20px; border: 1px solid #ddd; border-radius: 5px;'>
                        <h2 style='color: #333; text-align: center;'>Verification Successful!</h2>
                        <p>Congratulations! Your email address has been successfully verified.</p>
                        <p>
                          If you have any questions or encounter any issues, please don't hesitate to reach out to our support team at
                          <a href='mailto:support@topictalks.rawfin.net'>support@topictalks.rawfin.net</a>. We're here to help!
                        </p>
                        <p>Best,<br>TopicTalks Team</p>
                      </div>
                    </body>
                  """,
                isHtml: true);

        Send(email);
    }

    private void Send(IFluentEmail email)
    {
        try
        {
            email.SendAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}