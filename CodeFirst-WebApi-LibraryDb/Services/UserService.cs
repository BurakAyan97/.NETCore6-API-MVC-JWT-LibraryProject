using System.Security.Claims;

namespace CodeFirst_WebApi_LibraryDb.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
        }

        public string GetMyName()
        {
            var results = string.Empty;

            if (httpContextAccessor != null)
            {
                results = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return results;
        }
    }
}
