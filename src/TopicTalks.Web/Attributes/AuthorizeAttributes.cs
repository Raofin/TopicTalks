using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Web.Enums;

namespace TopicTalks.Web.Attributes;

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

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class RedirectIfAuthenticatedAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (filterContext.HttpContext.User.Identity is { IsAuthenticated: true })
        {
            filterContext.Result = new RedirectResult("/");
        }
    }
}