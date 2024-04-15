using Microsoft.EntityFrameworkCore;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Interfaces;

namespace TopicTalks.Infrastructure.Persistence.Repositories;

internal class OtpRepository(AppDbContext dbContext) : Repository<Otp>(dbContext), IOtpRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Otp?> GetOtpAsync(string email, string otp)
    {
        return await _dbContext.Otps
            .Where(o => o.Email == email && o.Code == otp && o.ExpiresAt > DateTime.Now)
            .FirstOrDefaultAsync();
    }
}