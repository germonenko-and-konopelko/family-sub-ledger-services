namespace GK.FSL.Common.Cryptography.Options;

public class HashingOptions
{
    public const int DefaultIterationsCount = 10_000;

    public int IterationsCount { get; set; } = DefaultIterationsCount;
}