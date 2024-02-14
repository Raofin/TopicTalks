#pragma warning disable CS8603 // Possible null reference return.

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using OSL.DAL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using OSL.BLL.Enums;

namespace OSL.BLL.Services;

internal class AuthService(IHttpContextAccessor http) : IAuthService
{
    public string UserId => http.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
    public string UserEmail => http.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
    public string UserRole => http.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value.ToString();

    public async Task Authenticate(User user)
    {
        var dateTimeNow = DateTime.Now;

        var claims = new List<Claim> {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),  // Subject (user ID)
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Iss, "https://rawfin.net"),  // Issuer
            new(JwtRegisteredClaimNames.Exp, new DateTimeOffset(dateTimeNow.AddDays(7)).ToUnixTimeSeconds().ToString()),  // Expiration Time
            new(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(dateTimeNow).ToUnixTimeSeconds().ToString()),  // Not Before
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(dateTimeNow).ToUnixTimeSeconds().ToString()),  // Issued At
            new(ClaimTypes.Role, user.UserRoles.FirstOrDefault()?.Role?.RoleName ?? RoleType.Student.ToString())
        };

        var authProperties = new AuthenticationProperties {
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.Now.AddDays(7),
            IsPersistent = true
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await http.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }
}
