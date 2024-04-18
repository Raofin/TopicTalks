namespace TopicTalks.Web.Services.Interfaces;

public interface IAuthService
{
    string GenerateJwtToken();
    Task SignInWithTokenAsync(string token);
    Task SignOutAsync();
}