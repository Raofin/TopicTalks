using Microsoft.AspNetCore.Authorization;
using TopicTalks.Domain.Enums;

namespace TopicTalks.Api.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeStudentAttribute : AuthorizeAttribute
{
    public AuthorizeStudentAttribute()
    {
        Roles = nameof(RoleType.Student);
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeTeacherAttribute : AuthorizeAttribute
{
    public AuthorizeTeacherAttribute()
    {
        Roles = nameof(RoleType.Teacher);
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeModeratorAttribute : AuthorizeAttribute
{
    public AuthorizeModeratorAttribute()
    {
        Roles = nameof(RoleType.Moderator);
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params RoleType[] roles)
    {
        Roles = string.Join(",", roles);
    }
}