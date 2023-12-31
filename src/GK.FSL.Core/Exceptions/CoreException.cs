namespace GK.FSL.Core.Exceptions;

public class CoreException(string message, string? description = null, Exception? innerException = null)
    : Exception(message, innerException)
{
    public string? Description { get; } = description;
}