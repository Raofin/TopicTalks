namespace TopicTalks.Web.Services;

public interface IAuthService
{
    string GenerateJwtToken();
    Task SignInWithTokenAsync(string token);
    Task SignOutAsync();
}