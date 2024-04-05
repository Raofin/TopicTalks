using TopicTalks.Web.Configs;
using TopicTalks.Web.Services;
using WebMarkupMin.AspNetCore8;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppSettingFetcher();

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddAuthConfig();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IHttpService, HttpService>();

builder.Services.AddMvc();
builder.Services.InitializeWebOptimizer(builder);
builder.Services.InitializeWebMarkupMin();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowOrigin",
        policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

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

app.MapHealthChecks("health");
app.UseStaticFiles();
app.UseWebOptimizer();
app.UseWebMarkupMin();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowOrigin");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();