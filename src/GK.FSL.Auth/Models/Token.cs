namespace GK.FSL.Auth.Models;

public record Token(string Value, DateTimeOffset Expires);