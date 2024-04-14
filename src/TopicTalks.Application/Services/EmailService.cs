using FluentEmail.Core;
using TopicTalks.Application.Interfaces;

namespace TopicTalks.Application.Services;

internal class EmailService(IFluentEmail fluentEmail) : IEmailService
{
    private readonly IFluentEmail _fluentEmail = fluentEmail;

    public async Task SendWelcome(string emailAddress)
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

        await SendAsync(email);
    }

    private async Task SendAsync(IFluentEmail email)
    {
        try
        {
            await email.SendAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}