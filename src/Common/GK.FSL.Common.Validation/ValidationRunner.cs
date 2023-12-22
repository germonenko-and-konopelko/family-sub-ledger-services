using GK.FSL.Common.Validation.Contracts;

namespace GK.FSL.Common.Validation;

public class ValidationRunner<TModel>(
    IEnumerable<IValidator<TModel>> validators,
    IEnumerable<IAsyncValidator<TModel>> asyncValidators
) : IValidationRunner<TModel>
{
    public async Task<bool> TryRunValidationAsync(TModel model, ICollection<ValidationError> errors)
    {
        var valid = true;

        foreach (var validator in validators)
        {
            if (!validator.TryValidate(model, errors))
            {
                valid = false;
            }
        }

        foreach (var validator in asyncValidators)
        {
            if (!await validator.TryValidateAsync(model, errors))
            {
                valid = false;
            }
        }

        return valid;
    }

    public async Task EnsureModelIsValidAsync(TModel model, string? message = "Validation error occurred.")
    {
        var errors = new List<ValidationError>();
        if (!await TryRunValidationAsync(model, errors))
        {
            throw new ValidationException(errors, message);
        }
    }
}