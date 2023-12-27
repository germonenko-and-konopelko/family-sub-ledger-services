using System.Text.Json.Serialization;

namespace GK.FSL.Api.Modules.Authorization.Models;

public record AuthorizationResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("expires")]
    public long ExpiresTimestamp { get; set; }
}