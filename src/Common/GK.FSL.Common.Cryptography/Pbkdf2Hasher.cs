using GK.FSL.Common.Cryptography.Contracts;
using GK.FSL.Common.Cryptography.Options;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;

namespace GK.FSL.Common.Cryptography;

public class Pbkdf2Hasher(IOptions<HashingOptions> options) : IHasher
{
    public byte[] GetHash(string input, byte[] salt, int requestedLength)
    {
        var iterationsCount = options.Value.IterationsCount;
        return KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA256, iterationsCount, requestedLength);
    }
}