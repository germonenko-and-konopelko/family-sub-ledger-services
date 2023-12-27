using System.Numerics;

namespace GK.FSL.Common.Cryptography.Contracts;

public interface IEncoder<T> where T : unmanaged, IBinaryInteger<T>, IMinMaxValue<T>
{
    public string Encode(T source);
}