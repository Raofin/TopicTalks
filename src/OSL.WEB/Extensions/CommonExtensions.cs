using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc;

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
}