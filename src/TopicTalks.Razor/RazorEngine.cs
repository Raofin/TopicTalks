using Razor.Templating.Core;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Razor;

public class RazorEngine : IRazorEngine
{
    public async Task<string> RenderAsync(string template, dynamic model)
    {
        return await RazorTemplateEngine.RenderAsync(template, model);
    }
}