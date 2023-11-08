namespace Models.Authentication
{
    public class ResponseResetPasswordModel
    {
        public bool IsSuccess { get; set; }
        public TokenModel? TokenModel { get; set; }
    }
}
