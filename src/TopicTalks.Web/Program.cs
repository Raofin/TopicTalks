using Microsoft.AspNetCore.Authentication.Cookies;
using TopicTalks.Web.Configs;
using TopicTalks.Web.Enums;
using WebMarkupMin.AspNetCore8;

var builder = WebApplication.CreateBuilder(args);

builder.InitializeAppSettings();

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();


builder.Services.AddCors(options => {
    options.AddPolicy("AllowOrigin",
        policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
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
builder.Services.InitializeWebOptimizer(builder);
builder.Services.InitializeWebMarkupMin();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseWebOptimizer();
app.UseWebMarkupMin();


app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowOrigin");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();
