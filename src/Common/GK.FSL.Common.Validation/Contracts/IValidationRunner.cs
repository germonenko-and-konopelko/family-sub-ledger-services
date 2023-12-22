namespace GK.FSL.Common.Validation.Contracts;

/// <summary>
/// The validation runner is used to run all
/// registered validators for the specified model type.
/// </summary>
/// <typeparam name="TModel">
/// A type fo the model to be validated.
/// All <see cref="IValidator{TModel}"/> and <see cref="IAsyncValidator{TModel}"/> will be taken into account.
/// </typeparam>
public interface IValidationRunner<TModel>
{
    /// <summary>
    ///     Runs all validators registered for the model type.
    /// </summary>
    /// <param name="model">Model to be validated.</param>
    /// <param name="errors">Errors found during validation.</param>
    /// <returns>
    ///     <c>true</c> if the model is valid
    ///     or
    ///     <c>false</c> if the model is invalid
    /// </returns>
    public Task<bool> TryRunValidationAsync(TModel model, ICollection<ValidationError> errors);

    /// <summary>
    ///     Runs all validators registered for the model type.
    ///     But instead of returning a collection of errors it throws <see cref="ValidationException"/>
    ///     that contains all the errors found.
    /// </summary>
    /// <param name="model">Model to be validated.</param>
    /// <param name="message">Error message to be put into <see cref="ValidationException"/>.</param>
    /// <exception cref="ValidationError">
    ///     This exception is thrown when the model is invalid.
    /// </exception>
    public Task EnsureModelIsValidAsync(TModel model, string? message = "Validation error occurred.");
}