using TopicTalks.Web.ViewModels;

namespace TopicTalks.Web.Services.Interfaces;

public interface IAuthService
{
    string GenerateJwtToken();
    Task SignInAsync(AuthenticationResponse authentication);
    Task SignOutAsync();
}