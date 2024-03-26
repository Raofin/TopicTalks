using TopicTalks.Web.Enums;

namespace TopicTalks.Web.VIewModels;

public record LoginViewModel(
    long UserId,
    string Token,
    string Email,
    List<RoleType?> Role
);