using TopicTalks.Domain.Entities;

namespace TopicTalks.Domain.Interfaces.Repositories;

public interface IOtpRepository : IRepository<Otp>
{
    Task<Otp?> GetOtpAsync(string email);
    Task<Otp?> GetValidOtpAsync(string email, string otp);
    Task<List<Otp>> GetExpiredOtpsAsync();
}