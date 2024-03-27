namespace TopicTalks.Web.Services;

public interface IAuthService
{
    Task<bool> SignInWithTokenAsync(string token);
    Task SignOutAsync();
    string GenerateJwtToken();
}