using TopicTalks.Web;
using TopicTalks.Web.Common;
using TopicTalks.Web.Services;
using TopicTalks.Web.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddAppConfigurations(builder.Environment);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient(builder.Configuration);
builder.Services.AddMvc();

builder.Services.AddTransient<ITokenCacheService, TokenCacheService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IHttpService, HttpService>();

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

app.UseHttpsRedirection();
app.UseRouting();
app.UseHostFiltering();
app.UseCustomCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Dashboard}/{id?}");

app.Run();