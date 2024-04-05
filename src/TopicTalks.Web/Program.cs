using TopicTalks.Web.Configs;
using TopicTalks.Web.Services;
using WebMarkupMin.AspNetCore8;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddAuthConfig();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IHttpService, HttpService>();

builder.Services.AddMvc();
builder.Services.InitializeWebMarkupMin();
builder.InitializeWebOptimizer();

builder.AddCorsConfig();
builder.AddSettingFetcher();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseWebOptimizer();
app.UseWebMarkupMin();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCustomCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Dashboard}/{id?}");

app.Run();