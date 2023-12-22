namespace GK.FSL.Common.Cryptography;

public interface IHasher
{
    public byte[] GetHash(string input, byte[] salt, int requestedLength);
}