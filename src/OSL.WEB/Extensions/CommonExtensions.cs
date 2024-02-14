using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;

namespace OSL.WEB.Extensions;

public static class ModelStateExtensions
{
    public static BadRequestObjectResult ValidationFailed(this ModelStateDictionary modelState)
    {
        if (modelState.IsValid)
        {
            return null;
        }

        var errors = modelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));

        return new BadRequestObjectResult(new {
            ErrorMessage = "Validation failed",
            Errors = errors
        });
    }

    public static string ErrorDescription<T>(this ErrorOr<T> result)
    {
        return result.FirstError.Description ?? "Internal server error. Please try again later.";
    }
}