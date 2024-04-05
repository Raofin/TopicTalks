using NUglify.Css;
using NUglify.JavaScript;

namespace TopicTalks.Web.Configs;

public static class WebOptimizerConfig
{
    public static void InitializeWebOptimizer(this WebApplicationBuilder builder)
    {
        if (!builder.Environment.IsDevelopment())
        {
            builder.Services.AddWebOptimizer(pipeline => {
                    pipeline.AddCssBundle("/css/bundle.css", new CssSettings { MinifyExpressions = false }, "css/*.css");
                    pipeline.AddJavaScriptBundle("/js/bundle.js", new CodeSettings { MinifyCode = false }, "js/*.js");
                },
                option => {
                    option.EnableCaching = false;
                    option.EnableDiskCache = false;
                    option.EnableMemoryCache = false;
                });
        }
        else
        {
            builder.Services.AddWebOptimizer(pipeline => {
                pipeline.AddCssBundle("/css/bundle.css", "css/*.css");
                pipeline.AddJavaScriptBundle("/js/bundle.js", "js/*.js");
            });
        }
    }
}