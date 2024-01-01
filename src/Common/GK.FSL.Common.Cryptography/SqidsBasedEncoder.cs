using System.Numerics;
using GK.FSL.Common.Cryptography.Contracts;
using Microsoft.Extensions.Options;
using Sqids;

namespace GK.FSL.Common.Cryptography;

public class SqidsBasedEncoder<T>(IOptions<SqidsOptions> options)
    : IEncoder<T> where T : unmanaged, IBinaryInteger<T>, IMinMaxValue<T>
{
    private readonly SqidsEncoder<T> _encoder = new(options.Value);

    public string Encode(T source) => _encoder.Encode(source);
}