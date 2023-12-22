namespace GK.FSL.Common.Validation;

public class ValidationException : Exception
{
    public ICollection<ValidationError> Errors { get; }

    public ValidationException(
        ICollection<ValidationError> errors,
        string? message = null,
        Exception? innerException = null)
        : base(message, innerException)
    {
        Errors = errors;
    }
}