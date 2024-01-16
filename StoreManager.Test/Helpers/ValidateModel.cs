using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreManager.Test.Helpers;

public class ValidateResult
{
    public bool IsValid { get; init; }
    public List<ValidationResult> ValidationResults { get; init; } = null!;
}

public static class ValidateModel
{
    public static ValidateResult Validate(object model)
    {
        var validationContext = new ValidationContext(model, null, null);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

        return new ValidateResult
        {
            IsValid = isValid,
            ValidationResults = validationResults
        };
    }
}