namespace GK.FSL.Common.Validation.Contracts;

/// <summary>
/// This in the async version of <see cref="IValidator{TModel}"/>
/// </summary>
/// <typeparam name="TModel">A type of the model to be validated.</typeparam>
public interface IAsyncValidator<TModel>
{
    /// <summary>
    /// Validates the given model.
    /// </summary>
    /// <param name="model">A model to be validated.</param>
    /// <param name="errors">A collection of errors to push new errors to.</param>
    /// <returns>
    ///     <c>true</c> if the model is valid.
    ///     or
    ///     <c>false</c> if the model is invalid.
    /// </returns>
    public Task<bool> TryValidateAsync(TModel model, ICollection<ValidationError> errors);
}