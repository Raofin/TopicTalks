using FluentValidation.Results;

namespace TopicTalks.Application.Extensions;

public static class ValidationExtensions
{
    public static dynamic ToDto(this ValidationResult validationResult)
    {
        var groupedErrors = validationResult.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g
                .Select(e => e.ErrorMessage)
                .ToList()
            );

        return new { Errors = groupedErrors };
    }
}