using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TopicTalks.Domain;

namespace TopicTalks.Application.BackgroundServices;

internal class OtpCleanupService(IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var otps = await unitOfWork.Otp.GetExpiredOtpsAsync();

                unitOfWork.Otp.RemoveRange(otps);

                await unitOfWork.CommitAsync();
            }
        }
    }
}