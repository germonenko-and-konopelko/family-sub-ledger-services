using System.Text.Json.Serialization;

namespace GK.FSL.Api.Modules.Authorization.Models;

public record AuthorizationResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = "Bearer";

    [JsonPropertyName("expires")]
    public long ExpiresTimestamp { get; set; }

    public static AuthorizationResponse GetSwaggerExample() => new()
    {
        AccessToken =
            "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzaWQiOiJLREwxTFNXRUg2Iiwic3ViIjoiN1hNMUxQSVdTWiIsImVtYWlsIjoiZW1pbGlhLm3DvGxsZXJAZ21haWwuY29tIiwiZ2l2ZW5fbmFtZSI6IkVtaWxpYSIsImZhbWlseV9uYW1lIjoiTcO8bGxlciIsImV4cCI6MTcwMzg3NjcyNywiaWF0IjoxNzAzODc2NjY3LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjMwMDAiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjMwMDAifQ",
        RefreshToken = "pPvA3zkeUxIXpUjkWyfuoQ==",
        ExpiresTimestamp = DateTimeOffset.UtcNow.AddMinutes(1).ToUnixTimeMilliseconds()
    };
}