using Microsoft.AspNetCore.Http;

namespace API.Services
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _context;

        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetUid()
        {
            return _context.HttpContext?.User.FindFirst("user_id")?.Value;
        }
    }
}