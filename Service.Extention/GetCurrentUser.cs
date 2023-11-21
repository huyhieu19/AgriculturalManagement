using Microsoft.AspNetCore.Http;

namespace Service.Extensions
{
    public class GetCurrentUser
    {
        private static readonly IHttpContextAccessor _contextAccessor;
        public GetCurrentUser()
        {
        }
        public static string GetUser()
        {
            var UserName = _contextAccessor.HttpContext!.User.FindFirst("UserName")!.Value;
            return UserName;
        }
    }
}
