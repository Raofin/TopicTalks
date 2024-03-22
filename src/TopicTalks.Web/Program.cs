using Microsoft.AspNetCore.Authentication.Cookies;
using TopicTalks.Web.Enums;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();


builder.Services.AddCors(options => {
    options.AddPolicy("AllowOrigin",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services
    .AddAuthorization(options => {
        options.AddPolicy(RoleType.Student.ToString(), policy => policy.RequireRole(RoleType.Student.ToString()));
        options.AddPolicy(RoleType.Teacher.ToString(), policy => policy.RequireRole(RoleType.Teacher.ToString()));
        options.AddPolicy(RoleType.Moderator.ToString(), policy => policy.RequireRole(RoleType.Moderator.ToString()));
    })
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.Cookie.Name = "TT_Cookies";
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/401";

        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });

builder.Services.AddMvc();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowOrigin");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();
