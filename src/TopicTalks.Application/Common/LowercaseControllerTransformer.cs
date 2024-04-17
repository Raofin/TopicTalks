using Microsoft.AspNetCore.Routing;

namespace TopicTalks.Application.Common;

public class LowercaseControllerTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return value?.ToString()?.ToLower();
    }
}