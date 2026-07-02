using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using University.Core.Forms;
using University.Core.Services;
namespace University.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ApiResponse> Register([FromBody] RegisterForm form)
        {
            var result = await _authService.RegisterAsync(form);
            return new ApiResponse(result);
        }

        [HttpPost("login")]
        public async Task<ApiResponse> Login([FromBody] LoginForm form)
        {
            var result = await _authService.LoginAsync(form);
            return new ApiResponse(result);
        }

        [HttpGet("me")]
        [Authorize]
        public ApiResponse GetMe()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var userRole = User.FindFirstValue(ClaimTypes.Role);

            return new ApiResponse(new
            {
                Id = userId,
                Email = userEmail,
                Role = userRole
            });
        }
    }
}
