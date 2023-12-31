namespace GK.FSL.Auth.Models;

public record AuthorizationResult(Token AccessToken, Token RefreshToken);