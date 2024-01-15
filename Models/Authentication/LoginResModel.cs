namespace Models.Authentication
{
    public record LoginResModel(TokenModel? Token = null, ProfileUser? Profile = null, bool IsSuccessed = true);
}
