using TopicTalks.Domain.Entities;

namespace TopicTalks.Domain.Interfaces;

public interface IOtpRepository : IRepository<Otp>
{
    Task<Otp?> GetOtpAsync(string email, string otp);
}