namespace Models
{
    public record TokenModel(string AccessToken, string RefreshToken);
    public record ProfileUser(string? Id, string? UserName, string? Email, string? Address, string? PhoneNumber);

}