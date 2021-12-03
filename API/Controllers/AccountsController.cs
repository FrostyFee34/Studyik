using System.Threading.Tasks;
using API.DTO;
using API.Errors;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountsController : BaseApiController
    {
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyToken(TokenVerifyRequest request)
        {
            var auth = FirebaseAuth.DefaultInstance;
            try
            {
                var response = await auth.VerifyIdTokenAsync(request.Token);
                if (response != null)
                    return Accepted();
            }
            catch (FirebaseException ex)
            {
                return BadRequest(new ApiException(400, "Failed to authenticate", ex.StackTrace));
            }

            return BadRequest(new ApiException(400, "Failed to authenticate"));
        }
    }
}