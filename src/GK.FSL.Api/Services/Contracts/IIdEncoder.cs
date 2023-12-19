namespace GK.FSL.Api.Services.Contracts;

public interface IIdEncoder
{
    public string Encode(long source);
}