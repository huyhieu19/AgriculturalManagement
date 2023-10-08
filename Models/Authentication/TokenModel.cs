namespace Models;

public record TokenModel(string AccessToken, string RefreshToken);
public record ProfileUser(string? UserName, string? Email);