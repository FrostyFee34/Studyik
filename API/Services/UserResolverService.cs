namespace API.Services
{
    using System.Security.Claims;
    using System.Security.Principal;
    using Microsoft.AspNetCore.Http;

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
        public string GetEmails()
        {
            return _context.HttpContext?.User.FindFirst("emails")?.Value;
        }
    }
}