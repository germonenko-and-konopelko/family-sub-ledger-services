namespace GK.FSL.Api.Constants;

public static class EventIds
{
    public static readonly EventId BadRequest = new(10404, "Bad Request");
    public static readonly EventId ServerError = new(10500, "Server Error");
}