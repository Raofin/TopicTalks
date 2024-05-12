using Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Razor;

public static class Dependencies
{
    public static IServiceCollection AddRazor(this IServiceCollection services, IHostEnvironment environment)
    {
        services.AddSingleton<IRazorEngine, RazorEngine>();
        services.AddRazorTemplating();

        if (environment.IsDevelopment())
        {
            services.AddMvcCore().AddRazorRuntimeCompilation();
            services.Configure<MvcRazorRuntimeCompilationOptions>(options =>
            {
                options.FileProviders.Add(new PhysicalFileProvider(
                    Path.Combine(Path.GetDirectoryName(environment.ContentRootPath)!, "TopicTalks.Razor", "Templates")
                ));
            });
        }

        return services;
    }
}