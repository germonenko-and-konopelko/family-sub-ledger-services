namespace GK.FSL.Common.Cryptography.Contracts;

public interface IHasher
{
    public byte[] GetHash(string input, byte[] salt, int requestedLength);
}