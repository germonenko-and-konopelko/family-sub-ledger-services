using GK.FSL.Api.Services.Contracts;
using Sqids;

namespace GK.FSL.Api.Services;

public class SqidsIdEncoder(SqidsEncoder<long> encoder) : IIdEncoder
{
    public string Encode(long source) => encoder.Encode(source);

    public long Decode(string source) => encoder.Decode(source).Single();
}