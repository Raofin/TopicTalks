namespace TopicTalks.Domain.Interfaces.Core;

public interface IRazorEngine
{
    Task<string> RenderAsync(string template, dynamic model);
}